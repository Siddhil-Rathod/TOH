# Tower_Of_Hanoi
A 2D Tower of Hanoi game made in Unity

Scenes:
1)MainMenu : Shows the main menu of the game with "Play" , "Rules " and " Quit" buttons.
2)Rules : Shows the Rules for playing the game
3)Game : The scene which contains
        a)The Tower of Hanoi challenge ( 5 discs and 3 poles)
        b)Restart button : To restart the game.
        c)Main Menu button : To go back to main menu scene.
        d)Quit Button : To quit the game.
        e)Moves : To display the number of moves made while playing the game.
        f)Timer : To display the time taken to complete the game.
        
Sprites:
1)"MainMenu_background" as the background image for the main menu scene
2)"Game_background" as the background for the game scene
3)"Rod" for the poles(towers) in the game
4)"discs" for the 5 discs in the game


TextMeshPro:
A package for text formatting

Scripts:
Language : C#
1)mainmenu.cs : Linked to the MainMenu scene
                Contains 3 functions
                a)PlayGame() -> To load the game scene.
                b)Rules() -> To load the rules scene.
                c)QuitGame() -> To quit the application.
                
2)Rules.cs : Linked to the Rules scene
             Contains a function back1() to go back to the MainMenu scene
             
             
3)Options.cs : Linked to the Game scene
                Functions:
                a)Timer() : To start the timer and continously update it during a game.
                b)Restart() : To restart the game
                c)MainMenu() : To go back to the main menu
                d) QuitGame() : To quit the application

4)Disc.cs : Linked to the 5 discs on the game scene
            Functions:
            OnMouseDown() , OnMouseDrag and OnMouseUp()
            Related to the dragging and dropping of disc and checking if the move is valid and upadting the moves counter
            Comments have been provided in the code explaining the same.
            
5)DiscPositions.cs : Linked to the game scene
            Functions:
            Contains the boiler plate start and update functions
            The update function has the UNDO functionality and the Win condition checking.
            Comments have been provided in the code explaining the same.
            
            
Thankyou.
