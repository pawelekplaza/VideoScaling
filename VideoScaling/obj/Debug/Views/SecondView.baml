   M S B A M L   `   `   `  ����G  CVideoScaling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:VideoScaling.ViewsVideoScaling.Views  5      #    VideoScaling.Views.SecondView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:VideoScaling.Views     5      $	��550��5   7   $	��300��6C   .��5	      � -   5          �Name$  
SecondGrid��5	      
������� � RowDefinitions 5
   
   �� 5      -   5         �Name$ FirstRow��5   (   $��94 �6   6(   �� 5      .+�5   
   �  5      �� 5      $��64 �6    �� 5      $��30 �6     � ColumnDefinitions 5      �� 5      $	��100 �6#   �� 5      $��1* �6#   .+�5      �� -   5         ��Name$ BrowseFileButton��5      $��	Browse...��6N   $
��Left=�6b   $��	20,20,0,0q�6}   $	��Top=�6�   $��75��6�   $��20��6�   ��6/   �� *
BrowseFile+6�   �� 5      -   5         ��Name$ FilePathTextBox��5      $��Stretch=�6P   $��20��6n   $��
20,20,20,0q�6z    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $
��Auto��6�   $��1��6�    �� 
IsReadOnly . $��Verdana)�6�   ��6/   �� *FilePathTextBox+6�   �� 5      -   5        $��CurrentFrame��5      $��1��6,   $��1��69   $��	220,0,0,0q�6p   $
��Left=�6�   $��Center=�6�   $��20��6�   $	��110��6�   $��Verdana)�6�   $��11(�6�   ��6I   �� *CurrentFrameTextBlock+6�   �� 5      -   5        $ ProceedButton��5      $��
Proceed...��6R   $��1��6g   $��Right=�6w   $��0,0,20,0q�6�   $��1��6�   $��Center=�6�   $��75��6�   $��3q�6�   $��5q�6  ��6,   �� *OpenProceedWindow+{�6�   �� *ProceedIsEnabled+6  �� 5      $
��Left=�6   $��20,0,0,0q�63   $��1��6E   $
��Wrap=�6R   $��Select frame:��6f   $��Center=�6{   $��Verdana)�6�   $��11(�6�   �� 5       -   5        $ PreviousFrameButton��5       $��Previous��6Z   $��1��6m   $
��Left=�6}   $��20,0,0,0q�6�   $��1��6�   $��Center=�6�   $��75��6�   ��62   �� *SelectPreviousFrame+{�6�   �� *PreviousFrameIsEnabled+�� 5!      -   5        $ NextFrameButton��5!      $
��Next��6S   $��1��6b   $
��Left=�6r   $��	120,0,0,0q�6�   $��1��6�   $��Center=�6�   $��75��6�   ��6.   �� *SelectNextFrame+{�6�   �� *NextFrameIsEnabled+5%      �� 5'   
   -	   5        $��FrameBorder��5'      $��2��6$   $��2��61   ���	#FF000000$��1,1,1,1q�6]   $��20, 10, 20, 10q�6w   ._�5(      �� -
   5        $��RectangleCanvas��5(      $��2��6,   $��2��69   .+�5)      �� -   5         ��Name$ Frame��5)      $��2��6F   $��Stretch=�6Z   $
��Auto��6x   $��1,1,1,1q�6�   $��Stretch=�6�   $
��Auto��6�   ��6'   �� *ImageSource+6�   5*      5+      �� 5,   
   -   5        $ PreviousVideoButton��5,      $��2��6P   $��	0,10,0,10q�6]   $��<��6p   $
��Left=�6   $��Stretch=�6�   $��15��6�   $
��Auto��6�   ��6.   �� *PreviousVideo+6�   5.      5/      