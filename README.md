ofxUnity
========

openFrameworks compiled as a native Unity plug in. Nothing more implemented than a proof of concept compiling and drawing a simple mesh. Still seeing strange crashing...	

This is still very incomplete.

Requires 'no-fmod' branch of openFrameworks: https://github.com/obviousjim/openFrameworks/tree/no-fmod

If you have a clone of openFrameworks do the following to switch to the right branch

	$ cd openFrameworks/
	$ git remote add obviousjim https://github.com/obviousjim/openFrameworks.git
	$ git fetch obviousjim
	$ git checkout -b no-fmod obviousjim/no-fmod

Then RenderPlugin.xcodeproj should Build, creating the RenderPlugin.bundle target

After making modifications, right click the .bundle and Reveal in Finder

Copy this into UnityProject/Assets/Plugins and restart Unity

Run Unity!

TODO:
Create ofAppUnityWindow

Pass Unity camera transform

Pass Mouse and key info
