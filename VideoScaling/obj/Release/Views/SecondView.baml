   M S B A M L   `   `   `  ���� G  CVideoScaling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:VideoScaling.ViewsVideoScaling.Views  #    VideoScaling.Views.SecondView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:VideoScaling.Views     $	��550��$	��300��.��� -     �Name$  
SecondGrid��
������� � RowDefinitions �� -    �Name$ FirstRow��$��94 ��� .+��  �� $��64 ��� $��30 � � ColumnDefinitions �� $	��100 ��� $��1* �.+��� -    ��Name$ BrowseFileButton��$��	Browse...��$
��Left=�$��	20,20,0,0q�$	��Top=�$��75��$��20������ *
BrowseFile+�� -    ��Name$ FilePathTextBox��$��Stretch=�$��20��$��
20,20,20,0q� �� TextWrapping$
 Wrap=�$	��Top=�$
��Auto��$��1�� �� 
IsReadOnly . $��Verdana)����� *FilePathTextBox+�� -   $��CurrentFrame��$��1��$��1��$��	220,0,0,0q�$
��Left=�$��Center=�$��20��$	��110��$��Verdana)�$��11(����� *CurrentFrameTextBlock+�� -   $ ProceedButton��$��
Proceed...��$��1��$��Right=�$��0,0,20,0q�$��1��$��Center=�$��75��$��3q�$��5q����� *OpenProceedWindow+{��� *ProceedIsEnabled+�� $
��Left=�$��20,0,0,0q�$��1��$
��Wrap=�$��Select frame:��$��Center=�$��Verdana)�$��11(��� -   $ PreviousFrameButton��$��Previous��$��1��$
��Left=�$��20,0,0,0q�$��1��$��Center=�$��75������ *SelectPreviousFrame+{��� *PreviousFrameIsEnabled+�� -   $ NextFrameButton��$
��Next��$��1��$
��Left=�$��	120,0,0,0q�$��1��$��Center=�$��75������ *SelectNextFrame+{��� *NextFrameIsEnabled+�� -	   $��FrameBorder��$��2��$��2�����	#FF000000$��1,1,1,1q�$��20, 10, 20, 10q�._��� -
   $��RectangleCanvas��$��2��$��2��.+��� -    ��Name$ Frame��$��2��$��Stretch=�$
��Auto��$��1,1,1,1q�$��Stretch=�$
��Auto������ *ImageSource+�� -   $ PreviousVideoButton��$��2��$��	0,10,0,10q�$��<��$
��Left=�$��Stretch=�$��15��$
��Auto������ *PreviousVideo+