   M S B A M L   `   `   `  ����G  CFileExplorer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FileExplorerFileExplorer  5   	       FileExplorer.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:FileExplorer     5   	   -   5          � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��250 �6   �� 5      $��5 �6   �� 5      $��* �6   .+�5   
   R� -   5         R�Name$ 
FolderView��5       � 	Resources 5       �� 
TargetType �� Property �� Value �� Orientation �� RelativeSource	 �� Mode
 �� AncestorType �� Path)   #FileExplorer.HeaderToImageConverter   Instance �� 	Converter��%G  'Q�       �� 5      " Q�6   .�5      ��  � �� 5      �� 5      .R�5   "   �� $ 
Horizontal=�6-   .+�5   &   �� $��20��6,   $��3q�5   ,   ��5   ,   ��  �� $	 FindAncestor=�"
 Q�$	 Tag�# Z �� 5   &   $��Bottom=�60   ��5   0   �� 5   #   5       5"      5#      5$      5%      � 5'   
   $��2��6   .+�5(      �� -   5         ��Name$ myList��5(      $��Stretch=�5)       �� AlternationCount$ 2��5*      $��Stretch=�5+      $��Stretch=�5,      $��Disabled=�5-      ���	#FFE5E5E5
������  � ContextMenu 53      �� 54      .@�55      m� -   5        $
��Open��55   #   61   m� 56      -   5        $
��Copy��56   #   61   m� 57      -   5        $��Delete��57   #   63   m� 58      -   5        $��Paste��58   #   62   m� 59      -   5        $	��Cut��59   #   60   m� 5:      -	   5        $	��New��5:   #   60   5;      ��5>      �� 5?      .R�5@      �� $
��left=�6%   $	��100��5A   %   $ Vertical=�5B   %   .+�5C      �� $��20��5D   $   $��Center=�5E   $   ��5C   $   �� *	FileImage+5E   $   �� 5G      $��1��6$   $��Center=�5I   $   ��5H   $   �� *
FileName+5I   $   5J      5K      ��5M      �� 5N      .R�5O      �  � Orientation$ 
Horizontal=�6$    � 	ItemWidth$	 100��5P   $   66   5Q      5T      5U      5X      5Y      