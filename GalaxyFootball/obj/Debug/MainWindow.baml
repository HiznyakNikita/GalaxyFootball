   M S B A M L   `   `   `  ����I  EGalaxyFootball, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:GalaxyFootball.UserControlsGalaxyFootball.UserControls  5   	       GalaxyFootball.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   7c)clr-namespace:GalaxyFootball.UserControls     5   	   -   5          � Title$  
MainWindow��5   	   $	��700��6   $
��1050��6)    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�6   ��5      �� 5	   
    �� ImageSource$  /Resources/image/grass.jpg��6   6@   .��5      �� -   5         ��Name$ PlaygroundCanvas��5      .+�5   
   ,   &GalaxyFootball.UserControls.BallMarker  -   5          Name$ 
ballMarker��5   )    K� ZIndex$ 3��6   $��25��6=   $��25��6I   
 �� Top 5      ��  �� Path$ Game.Ball.Position.Y�	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� Left
 5      �� $ Game.Ball.Position.X�$	 PropertyChanged=��� 5   
   $��14(�6O   
�������$
 10��6p   $ 10��6�   $	��250��6�   $��26��6�   ��6   �� *Score+$	 PropertyChanged=�6�   3   -GalaxyFootball.UserControls.PlayerMarkerBlack  5   
   -   5          Name$ playerAwayMarker1��5      $��25��69   $��25��6E    � DataContext 6P   �� *Game.TeamAway.Players[0]+
 5   	   �� *
Position.X+ 6,   �� *
Position.Y+  5   
   -   5        $ playerAwayMarker2��5      $��25��69   $��25��6E    6P   �� *Game.TeamAway.Players[1]+
 5   	   �� *
Position.X+ 6,   �� *
Position.Y+  5   
   -   5        $ playerAwayMarker3��5      $��25��6:   $��25��6F    6Q   �� *Game.TeamAway.Players[2]+
 5      �� *
Position.X+ 6B   �� *
Position.Y+  5   
   -   5        $ playerAwayMarker4��5      $��25��6:   $��25��6F    6Q   �� *Game.TeamAway.Players[3]+
 5      �� *
Position.X+ 6B   �� *
Position.Y+  5   
   -   5        $ playerAwayMarker5��5      $��25��6:   $��25��6F    6Q   �� *Game.TeamAway.Players[4]+
 5      �� *
Position.X+ 6B   �� *
Position.Y+  5   
   -	   5        $ playerAwayMarker6��5      $��25��6:   $��25��6F    6Q   �� *Game.TeamAway.Players[5]+
 5      �� *
Position.X+ 6B   �� *
Position.Y+  5   
   -
   5        $ playerAwayMarker7��5      $��25��6:   $��25��6F    6Q   �� *Game.TeamAway.Players[6]+
 5       �� *
Position.X+ 6B   �� *
Position.Y+  5!   
   -   5        $ playerAwayMarker8��5!      $��25��6:   $��25��6F    6Q   �� *Game.TeamAway.Players[7]+
 5"      �� *
Position.X+ 6B   �� *
Position.Y+  5#   
   -   5        $ playerAwayMarker9��5#      $��25��6:   $��25��6F    6Q   �� *Game.TeamAway.Players[8]+
 5$      �� *
Position.X+ 6B   �� *
Position.Y+  5%   
   -   5        $ playerAwayMarker10��5%      $��25��6;   $��25��6G    6R   �� *Game.TeamAway.Players[9]+
 5&      �� *
Position.X+ 6B   �� *
Position.Y+  5'   
   -   5        $ playerAwayMarker11��5'      $��25��6;   $��25��6G    6R   �� *Game.TeamAway.Players[10]+
 5(      �� *
Position.X+ 6B   �� *
Position.Y+1   +GalaxyFootball.UserControls.PlayerMarkerRed  5)   
   -   5          Name$ playerHomeMarker1��5)      $��25��68   $��25��6D    6O   �� *Game.TeamHome.Players[0]+
 5*      �� *
Position.X+ 6@   �� *
Position.Y+  5+   
   -   5        $ playerHomeMarker2��5+      $��25��68   $��25��6D    6O   �� *Game.TeamHome.Players[1]+
 5,      �� *
Position.X+ 6@   �� *
Position.Y+  5-   
   -   5        $ playerHomeMarker3��5-      $��25��68   $��25��6D    6O   �� *Game.TeamHome.Players[2]+
 5.      �� *
Position.X+ 6@   �� *
Position.Y+  5/   
   -   5        $ playerHomeMarker4��5/      $��25��68   $��25��6D    6O   �� *Game.TeamHome.Players[3]+
 50      �� *
Position.X+ 6@   �� *
Position.Y+  51   
   -   5        $ playerHomeMarker5��51      $��25��68   $��25��6D    6O   �� *Game.TeamHome.Players[4]+
 52      �� *
Position.X+ 6@   �� *
Position.Y+  53   
   -   5        $ playerHomeMarker6��53      $��25��68   $��25��6D    6O   �� *Game.TeamHome.Players[5]+
 54      �� *
Position.X+ 6@   �� *
Position.Y+  55   
   -   5        $ playerHomeMarker7��55      $��25��68   $��25��6D    6O   �� *Game.TeamHome.Players[6]+
 56      �� *
Position.X+ 6@   �� *
Position.Y+  57   
   -   5        $ playerHomeMarker8��57      $��25��68   $��25��6D    6O   �� *Game.TeamHome.Players[7]+
 58      �� *
Position.X+ 6@   �� *
Position.Y+  59   
   -   5        $ playerHomeMarker9��59      $��25��68   $��25��6D    6O   �� *Game.TeamHome.Players[8]+
 5:      �� *
Position.X+ 6@   �� *
Position.Y+  5;   
   -   5        $ playerHomeMarker10��5;      $��25��69   $��25��6E    6P   �� *Game.TeamHome.Players[9]+
 5<      �� *
Position.X+ 6@   �� *
Position.Y+  5=   
   -   5        $ playerHomeMarker11��5=      $��25��69   $��25��6E    6P   �� *Game.TeamHome.Players[10]+
 5>      �� *
Position.X+ 6@   �� *
Position.Y+5?      5@      