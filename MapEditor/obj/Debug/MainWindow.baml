   M S B A M L   `   `   `  ����D  @MapEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MapEditor	MapEditor  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       MapEditor.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MapEditor     5   	   ;assembly&clr-namespace:System;assembly=mscorlib     5   	     � Title$  M图编辑器��5	   	   $��577.6��6   $
��1400��6&    � 	Resources 5
         sexEnum S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� Type   MapEditor.LayerEnum��%i   &	        S� 5   
   $ 	GetValues��6-   "  5       5      M� 5      "  6   5      5      .��5      � .+�5   
   �� $
��Left=�6   $	��537��60   $	��Top=�6=   $	��180��6U   .+�5      �� -   5         ��Name$ groupBox��5      $��GroupBox��6)   $	��100��6;   .��5      � $
��Left=�6   $��98��62   $��0,-14,-2,-6q�6>   $	��Top=�6S   $	��170��6k   .+�5      �� -   5         ��Name$ InputAll��5      $��导入所有��6/   $
��Left=�6>   $��	10,22,0,0q�6Y   $	��Top=�6l   $��74��6�   6�   �� 5      -   5        $ AddKey��5      $��增加字段��6-   $
��Left=�6<   $��	94,22,0,0q�6W   $	��Top=�6j   $��76��6�   6�   �� 5      -   5        $ 	RemoveKey��5      $��删除字段��60   $
��Left=�6?   $��	94,64,0,0q�6Z   $	��Top=�6m   $��76��6�   6�   �� 5      -   5        $ 
ModifyData��5      $��批量修改��61   $
��Left=�6@   $��8,64,0,0q�6[   $	��Top=�6m   $��76��6�   6�   5      5      R� 5      -   5         R�Name$ treeView��5      $	��437��6)   ��6i   �� *root+��5      �� 5       	 �� DataType   MapEditor.TreeViewItem"	  6/   
 �� ItemsSource
 6V   ��  �� Path$ Children�.R�5!      ��  �� Orientation$ 
Horizontal=�6%   .+�5"      �� ��6(   �� *DisplayMapName+ � ToolTip 5#      �� 5$   "   ��6,   �� *	mapName+5&      5'      5)      5*      �� 5,   
   -   5         ��Name$ DetailPanel��5,      $
��Left=�6+   $	��312��6F   $��185,225,0,0q�6S   $	��Top=�6h   $
��1159��6�   .+�5-      � .+�5.      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5.         AutoGenerateColumns .  ��6-   ��    Columns 5/      0  *System.Windows.Controls.DataGridTextColumn  50      ,  &System.Windows.Controls.DataGridColumn   Header$ ColorR��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 6=   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *ColorR+  51      $ ColorG��6-   $ 80 6=    6H   �� *ColorG+  52      $ ColorB��6-   $ 80 6=    6H   �� *ColorB+  53      $
 Zmin��6-   $ 80 6;    6F   �� *Zmin+  54      $
 Zmax��6-   $ 80 6;    6F   �� *Zmax+4	  .System.Windows.Controls.DataGridComboBoxColumn	  55      $ 80 61   $ Layer��6<    	  SelectedItemBinding 56   )   �� *Layer+ 	  ItemsSource 57   )   ��  �� Source# [  4
  .System.Windows.Controls.DataGridCheckBoxColumn
  58      $ 
Pass Exam?��61   $	 100 6E    59   )   �� *Pass+5  /System.Windows.Controls.DataGridHyperlinkColumn  5:      $ Email��62   $	 150 6A    5;   *   �� *Email+5=      5>      5?      �� 5@   
   -	   5        $ inputCurrent��5@      $��导入当前��6'   $
��Left=�66   $��
243,96,0,0q�6Q   $	��Top=�6e   $��75��6}   6�   �� 5A   
   -
   5         ��Name$ mapName��5A      $
��Left=�6%   $��
243,31,0,0q�6@   $
��Wrap=�6T   $��地图名称��6h   $	��Top=�6t   $��19��6�   $��75��6�   �� 5B   
   -   5         ��Name$ mapImage��5B      $
��Left=�6"   $	��207��6=   $��
645,13,0,0q�6J   $	��Top=�6^   $	��226��6v   �� 5C   
   -   5        $ mapType��5C      $
��Left=�6%   $��
243,64,0,0q�6@   $
��Wrap=�6T   $#��副本/大世界/相位副本��6h   $	��Top=�6{   �� 5D   
   -   5        $ button��5D      $��添加数据��6!   $
��Left=�60   $��243,140,0,0q�6K   $	��Top=�6`   $��75��6x   6�   �� 5E   
   -   5        $ button1��5E      $��保存数据��6"   $
��Left=�61   $��361,140,0,0q�6L   $	��Top=�6a   $��75��6y   6�   5G      5H      