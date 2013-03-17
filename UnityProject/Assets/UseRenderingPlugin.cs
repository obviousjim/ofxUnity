using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class UseRenderingPlugin : MonoBehaviour {
	
	// Native plugin rendering events are only called if a plugin is used
	// by some script. This means we have to DllImport at least
	// one function in some active script.
	// For this example, we'll call into plugin's SetTimeFromUnity
	// function and pass the current time so the plugin can animate.
	[DllImport ("RenderingPlugin")]
	private static extern void SetTimeFromUnity(float t);
	
	[DllImport ("RenderingPlugin")]
	private static extern void SetMousePosition(float _x, float _y);
	
	[DllImport ("RenderingPlugin")]
	private static extern void SetWindowsShape(float _w, float _h);
	
	[DllImport ("RenderingPlugin")]
	private static extern void SetCameraPosition(float _x, float _y, float _z);
	
	[DllImport ("RenderingPlugin")]
	private static extern void SetSphereAt(float _x, float _y, float _z);
	
	[DllImport ("RenderingPlugin")]
	private static extern void SetCameraRotation(float _x, float _y, float _z, float _w);
	
	[DllImport ("RenderingPlugin")]
	private static extern void SetCameraFoV(float _Fov);
	
	[DllImport ("RenderingPlugin")]
	private static extern void SetCameraProjectionMatrixPain(float _m00, float _m01, float _m02, float _m03,
														 float _m10, float _m11, float _m12, float _m13,
														 float _m20, float _m21, float _m22, float _m23,
														 float _m30, float _m31, float _m32, float _m33);
	
	[DllImport ("RenderingPlugin")]
	private static extern void SetCameraWorldMatrixPain(float _m00, float _m01, float _m02, float _m03,
													float _m10, float _m11, float _m12, float _m13,
													float _m20, float _m21, float _m22, float _m23,
													float _m30, float _m31, float _m32, float _m33);
	
	// We'll also pass native pointer to a texture in Unity.
	// The plugin will fill texture data from native code.
	[DllImport ("RenderingPlugin")]
	private static extern void SetTextureFromUnity (System.IntPtr texture);

	
	public Transform theBall;

	
	IEnumerator Start () {
		CreateTextureAndPassToPlugin();
		yield return StartCoroutine("CallPluginAtEndOfFrames");
	}

	private void CreateTextureAndPassToPlugin()
	{
		// Create a texture
		Texture2D tex = new Texture2D(256,256,TextureFormat.ARGB32,false);
		// Set point filtering just so we can see the pixels clearly
		tex.filterMode = FilterMode.Point;
		// Call Apply() so it's actually uploaded to the GPU
		tex.Apply();

		// Set texture onto our matrial
		renderer.material.mainTexture = tex;

		// Pass texture pointer to the plugin
		SetTextureFromUnity (tex.GetNativeTexturePtr());
	}
	
	private void Update(){

		SetWindowsShape(Screen.width,Screen.height);
		SetMousePosition(Input.mousePosition.x,Input.mousePosition.y);
		
		var sPos = theBall.transform.position;
		SetSphereAt(sPos.x,sPos.y, sPos.z);
		print (sPos);
		
		Camera cam = Camera.mainCamera;
		Vector3 camPos = cam.gameObject.transform.position;
		Quaternion camRot =  cam.gameObject.transform.rotation;
		camRot *= Quaternion.AngleAxis(180.0f, Vector3.up);
		
		SetCameraPosition(camPos.x, camPos.y, camPos.z);
		SetCameraRotation(camRot.x,camRot.y,camRot.z,camRot.w);
		SetCameraFoV(cam.fov);
		
		Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3 (1,1,-1));
				
		//Matrix4x4 proM = cam.projectionMatrix.transpose;
		Matrix4x4 proM = GL.GetGPUProjectionMatrix(cam.projectionMatrix, false).transpose;
		SetCameraProjectionMatrixPain( 	proM.m00, proM.m01, proM.m02, proM.m03,
								   		proM.m10, proM.m11, proM.m12, proM.m13,
								   		proM.m20, proM.m21, proM.m22, proM.m23,
								   		proM.m30, proM.m31, proM.m32, proM.m33);
		//Camera.mainCamera.projectionMatrix;
		
		//Matrix4x4 LtW = cam.transform.localToWorldMatrix.transpose;
		//Matrix4x4 m = Matrix4x4.TRS(Quaternion.identity. transform.rotation.
		Matrix4x4 LtW = cam.worldToCameraMatrix.transpose;
		//Matrix4x4 LtW = GL.modelview.transpose;
		SetCameraWorldMatrixPain( 	LtW.m00, LtW.m01, LtW.m02, LtW.m03,
							  		LtW.m10, LtW.m11, LtW.m12, LtW.m13,
							  		LtW.m20, LtW.m21, LtW.m22, LtW.m23,
							  		LtW.m30, LtW.m31, LtW.m32, LtW.m33);
		//Camera.mainCamera.transform.localToWorldMatrix;
	}
	
	private IEnumerator CallPluginAtEndOfFrames (){
		while (true) {
			// Wait until all frame rendering is done
			yield return new WaitForEndOfFrame();
			
			// Set time for the plugin
			SetTimeFromUnity (Time.timeSinceLevelLoad);
			
			// Issue a plugin event with arbitrary integer identifier.
			// The plugin can distinguish between different
			// things it needs to do based on this ID.
			// For our simple plugin, it does not matter which ID we pass here.
			GL.IssuePluginEvent (1);
		}
	}
}
