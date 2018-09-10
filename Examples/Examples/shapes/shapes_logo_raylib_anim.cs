

using Raylib;

using static Raylib.Raylib;



public partial class Examples

{

    /*******************************************************************************************
    *
    *   raylib [shapes] example - raylib logo animation
    *
    *   This example has been created using raylib 1.4 (www.raylib.com)
    *   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
    *
    *   Copyright (c) 2014 Ramon Santamaria (@raysan5)
    *
    ********************************************************************************************/
    
    
    
    public static int shapes_logo_raylib_anim()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        int screenWidth = 800;
        int screenHeight = 450;
    
        InitWindow(screenWidth, screenHeight, "raylib [shapes] example - raylib logo animation");
    
        int logoPositionX = screenWidth/2 - 128;
        int logoPositionY = screenHeight/2 - 128;
    
        int framesCounter = 0;
        int lettersCount = 0;
    
        int topSideRecWidth = 16;
        int leftSideRecHeight = 16;
    
        int bottomSideRecWidth = 16;
        int rightSideRecHeight = 16;
    
        int state = 0;                  // Tracking animation states (State Machine)
        float alpha = 1.0f;             // Useful for fading
    
        SetTargetFPS(60);
        //--------------------------------------------------------------------------------------
    
        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            if (state == 0)                 // State 0: Small box blinking
            {
                framesCounter++;
    
                if (framesCounter == 120)
                {
                    state = 1;
                    framesCounter = 0;      // Reset counter... will be used later...
                }
            }
            else if (state == 1)            // State 1: Top and left bars growing
            {
                topSideRecWidth += 4;
                leftSideRecHeight += 4;
    
                if (topSideRecWidth == 256) state = 2;
            }
            else if (state == 2)            // State 2: Bottom and right bars growing
            {
                bottomSideRecWidth += 4;
                rightSideRecHeight += 4;
    
                if (bottomSideRecWidth == 256) state = 3;
            }
            else if (state == 3)            // State 3: Letters appearing (one by one)
            {
                framesCounter++;
    
                if (framesCounter/12)       // Every 12 frames, one more letter!
                {
                    lettersCount++;
                    framesCounter = 0;
                }
    
                if (lettersCount >= 10)     // When all letters have appeared, just fade out everything
                {
                    alpha -= 0.02f;
    
                    if (alpha <= 0.0f)
                    {
                        alpha = 0.0f;
                        state = 4;
                    }
                }
            }
            else if (state == 4)            // State 4: Reset and Replay
            {
                if (IsKeyPressed('R'))
                {
                    framesCounter = 0;
                    lettersCount = 0;
    
                    topSideRecWidth = 16;
                    leftSideRecHeight = 16;
    
                    bottomSideRecWidth = 16;
                    rightSideRecHeight = 16;
    
                    alpha = 1.0f;
                    state = 0;          // Return to State 0
                }
            }
            //----------------------------------------------------------------------------------
    
            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();
    
                ClearBackground(RAYWHITE);
    
                if (state == 0)
                {
                    if ((framesCounter/15)%2) DrawRectangle(logoPositionX, logoPositionY, 16, 16, BLACK);
                }
                else if (state == 1)
                {
                    DrawRectangle(logoPositionX, logoPositionY, topSideRecWidth, 16, BLACK);
                    DrawRectangle(logoPositionX, logoPositionY, 16, leftSideRecHeight, BLACK);
                }
                else if (state == 2)
                {
                    DrawRectangle(logoPositionX, logoPositionY, topSideRecWidth, 16, BLACK);
                    DrawRectangle(logoPositionX, logoPositionY, 16, leftSideRecHeight, BLACK);
    
                    DrawRectangle(logoPositionX + 240, logoPositionY, 16, rightSideRecHeight, BLACK);
                    DrawRectangle(logoPositionX, logoPositionY + 240, bottomSideRecWidth, 16, BLACK);
                }
                else if (state == 3)
                {
                    DrawRectangle(logoPositionX, logoPositionY, topSideRecWidth, 16, Fade(BLACK, alpha));
                    DrawRectangle(logoPositionX, logoPositionY + 16, 16, leftSideRecHeight - 32, Fade(BLACK, alpha));
    
                    DrawRectangle(logoPositionX + 240, logoPositionY + 16, 16, rightSideRecHeight - 32, Fade(BLACK, alpha));
                    DrawRectangle(logoPositionX, logoPositionY + 240, bottomSideRecWidth, 16, Fade(BLACK, alpha));
    
                    DrawRectangle(screenWidth/2 - 112, screenHeight/2 - 112, 224, 224, Fade(RAYWHITE, alpha));
    
                    DrawText(SubText("raylib", 0, lettersCount), screenWidth/2 - 44, screenHeight/2 + 48, 50, Fade(BLACK, alpha));
                }
                else if (state == 4)
                {
                    DrawText("[R] REPLAY", 340, 200, 20, GRAY);
                }
    
            EndDrawing();
            //----------------------------------------------------------------------------------
        }
    
        // De-Initialization
        //--------------------------------------------------------------------------------------
        CloseWindow();        // Close window and OpenGL context
        //--------------------------------------------------------------------------------------
    
        return 0;
    }

}