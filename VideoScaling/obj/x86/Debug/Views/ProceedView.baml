   M S B A M L   `   `   `  ����G  CVideoScaling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:VideoScaling.ViewsVideoScaling.Views  5      $    VideoScaling.Views.ProceedView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:VideoScaling.Views     5      .��5	      � -   5          �Name$  ProceedGrid��5	      
������� � RowDefinitions 5
   
   �� 5      $��45 �6   �� 5      $��45 �6   �� 5      .+�5   
   �  � ColumnDefinitions 5      �� 5      $	��100 �6#   �� 5      .+�5      �� -   5         ��Name$ BrowseButton��5      $��	Browse...��6U   $
��Left=�6i   $��	10,10,0,0q�6�   $	��Top=�6�   $��75��6�   $��20��6�   ��6+   �� *BrowseOutputDirectory+6�   �� 5      -   5         ��Name$ PathTextBox��5      $��1��6N   $��Stretch=�6^   $��20��6|   $��
5,10,10,10q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $
��Auto��6�    �� 
IsReadOnly . ��6+   �� *OutputPathTextBox+6�   5      � 5   
   $��2��6    5      �� 5      �� 5      $	��100 �6#   .+�5"      � -   5        $��ProgressBar��5"      $��Stretch=�6-   $��20��6K   $
��Auto��6W   $��10,10,10,10q�6d   $��Bottom=�6y    � Value 6�   �� *ProgressBarValue+ � Maximum 6�   �� *ProgressBarMax+�� 5#      -   5        $ StartButton��5#      $��Start��6D   $��1��6T   $��Right=�6d   $��
0,10,10,10q�6�   $��Bottom=�6�   $��75��6�   ��6*   �� *Start+6�   5'      �� 5,   
   -   5        	 ��Name$	 OpenNewVideoCheckBox��5,      $'��!Open new video file after process��6a   $
��Left=�6�   $��	10,10,0,0q�6�   $��1��6�   $��2��6�   $	��Top=�6�   
 d� 	IsChecked
 61   �� *OpenNewVideoFileIsChecked+6�   5.      5/      