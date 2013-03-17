//
//  ofUnityWindow.h
//  RenderingPlugin
//
//  Created by Patricio Gonzalez Vivo on 3/16/13.
//  Copyright (c) 2013 James George. All rights reserved.
//

#ifndef __RenderingPlugin__ofUnityWindow__
#define __RenderingPlugin__ofUnityWindow__

#include "ofMain.h"
#include "ofAppBaseWindow.h"

class ofBaseApp;
class ofUnityWindow : public ofAppBaseWindow {
public:
    
	ofUnityWindow(){};
    
    ~ofUnityWindow(){};
    
    void setupOpenGL(int w, int h, int screenMode) {}
    void initializeWindow() {}
    void runAppViaInfiniteLoop(ofBaseApp * appPtr) {}
    
    void hideCursor() {}
    void showCursor() {}
    
    void	setWindowPosition(int x, int y) {}
    void	setWindowShape(int w, int h) {
        width = w;
        height = h;
    }
    
    int		getFrameNum() { return 0; }
    float	getFrameRate() {return 0; }
    double  getLastFrameTime(){ return 0.0; }
    
    ofPoint	getWindowPosition() {return ofPoint(); }
    ofPoint	getWindowSize(){return ofPoint(width,height); }
    ofPoint	getScreenSize(){return ofPoint(width,height); }
    
    void			setOrientation(ofOrientation orientation){ }
    ofOrientation	getOrientation(){ return OF_ORIENTATION_DEFAULT; }
    bool	doesHWOrientation(){return false;}
    
	//this is used by ofGetWidth and now determines the window width based on orientation
    int		getWidth(){ return width; }
    int		getHeight(){ return width; }
    
    void	setFrameRate(float targetRate){}
    void	setWindowTitle(string title){}
    
    int		getWindowMode() {return 0;}
    
    void	setFullscreen(bool fullscreen){}
    void	toggleFullscreen(){}
    
    void	enableSetupScreen(){}
    void	disableSetupScreen(){}

private:
    float width;
    float height;
};

#endif /* defined(__RenderingPlugin__ofUnityWindow__) */
