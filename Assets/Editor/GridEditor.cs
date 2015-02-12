using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor {
	Grid grid;
	private static int oldIndex=0;

	//target value is given by the custom editor, notice the compiler does not complain about it
	void OnEnable(){
		grid = (Grid) target;
	}

	[MenuItem("Assets/Create/TileSet")]
	static void CreateTileSet(){
		var asset = ScriptableObject.CreateInstance<TileSet>();
		var path= AssetDatabase.GetAssetPath(Selection.activeObject);

		if(string.IsNullOrEmpty(path)){
			path="Assets";
		} else if (Path.GetExtension(path) != ""){
			path= path.Replace(Path.GetFileName(path),"");
		} else {
			path+="/";
		}
	

		var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "TileSet.asset");
		AssetDatabase.CreateAsset(asset,assetPathAndName);
		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
		asset.hideFlags= HideFlags.DontSave;
	}

	public override void OnInspectorGUI(){
		//base.OnInpspectorGUI(); //will add the original inpsector menu
		grid.width= createSlider ("Grid Width", grid.width);
		grid.height= createSlider ("Grid Height", grid.height);

		if(GUILayout.Button("Open Grid Window")){
			GridWindow window= (GridWindow) EditorWindow.GetWindow(typeof(GridWindow));
			window.init ();
		}

		//Tile Prefab
		EditorGUI.BeginChangeCheck();
		var newTilePrefab=(Transform) EditorGUILayout.ObjectField("TilePrefab", grid.tilePrefab, typeof(Transform),false);
		if(EditorGUI.EndChangeCheck()){
			grid.tilePrefab=newTilePrefab;
			Undo.RecordObject(target, "Grid Changed");
		}

		//Tile Map
		EditorGUI.BeginChangeCheck ();
		var newTileSet=(TileSet) EditorGUILayout.ObjectField ("TileSet", grid.tileset, typeof(TileSet), false);
		if(EditorGUI.EndChangeCheck()){
			grid.tileset=newTileSet;
			Undo.RecordObject (target, "TileSet Changed");
		}

		if (grid.tileset != null){
			EditorGUI.BeginChangeCheck();
			var names= new string[grid.tileset.prefabs.Length];
			var values=new int[names.Length];

			for(int i=0; i<names.Length; i++){
				names[i]= grid.tileset.prefabs[i] != null ? grid.tileset.prefabs[i].name : "";
				values[i]=i;
			}

			var index= EditorGUILayout.IntPopup("Select Tile", oldIndex, names, values);

			if (EditorGUI.EndChangeCheck()){
				Undo.RecordObject(target,"Grid Changed");
				if(oldIndex != index){
					oldIndex=index;
					grid.tilePrefab = grid.tileset.prefabs[index];

					float width= grid.tilePrefab.renderer.bounds.size.x;
					float height= grid.tilePrefab.renderer.bounds.size.y;

					grid.width=width;
					grid.height=height;
				}
			}
			
		}
	}

	float createSlider (string labelName, float sliderPosition)
	{
		GUILayout.BeginHorizontal ();
		//creates a horizontal group
		
		GUILayout.Label (labelName);
		sliderPosition = EditorGUILayout.Slider (sliderPosition, 1f, 100f, null);
		GUILayout.EndHorizontal ();
		return sliderPosition;
	}

	//is called when a event on the scene is recorded, e.g. click etc
	void OnSceneGUI(){
		int controlId=GUIUtility.GetControlID(FocusType.Passive);
		Event e= Event.current;
		Ray ray=Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
		Vector3 mousePos= ray.origin;

		if(e.isMouse && e.type==EventType.MouseDown){
			GUIUtility.hotControl = controlId;
			e.Use();

			GameObject gameObject;
			//grab the currently selected prefab
			Transform prefab = grid.tilePrefab;
			if(prefab){
				Undo.IncrementCurrentGroup();
				gameObject=(GameObject) PrefabUtility.InstantiatePrefab(prefab.gameObject);
				Vector3 alligned = new Vector3(Mathf.Floor(mousePos.x/grid.width)*grid.width +grid.width/2,Mathf.Floor(mousePos.y/grid.height)*grid.height + grid.height/2, 0f);
				gameObject.transform.position = alligned;
				gameObject.transform.parent = grid.transform;
				Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
			}
		}

		if(e.isMouse && e.type==EventType.MouseUp){
			GUIUtility.hotControl=0;
		}
	}
}