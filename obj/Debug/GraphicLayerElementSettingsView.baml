   M S B A M L   `   `   `  ����R  NAscon.Pilot.Theme, Version=20.35.0.34472, Culture=neutral, PublicKeyToken=nullbCclr-namespace:Ascon.Pilot.Theme.Controls;assembly=Ascon.Pilot.ThemeAscon.Pilot.Theme.Controls  5      \@clr-namespace:Ascon.Pilot.Theme.Tools;assembly=Ascon.Pilot.ThemeAscon.Pilot.Theme.Tools  5      U QGraphicLayerSample2-1.ext2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:Ascon.Pilot.SDK.GraphicLayerSample"Ascon.Pilot.SDK.GraphicLayerSample 5      m;clr-namespace:Ascon.Pilot.SDK.GraphicLayerSample.Properties-Ascon.Pilot.SDK.GraphicLayerSample.Properties 5	      H   BAscon.Pilot.SDK.GraphicLayerSample.GraphicLayerElementSettingsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      XcontrolsCclr-namespace:Ascon.Pilot.Theme.Controls;assembly=Ascon.Pilot.Theme     5      Rtools@clr-namespace:Ascon.Pilot.Theme.Tools;assembly=Ascon.Pilot.Theme     5      OgraphicLayerSample0clr-namespace:Ascon.Pilot.SDK.GraphicLayerSample     5      Ip;clr-namespace:Ascon.Pilot.SDK.GraphicLayerSample.Properties     5	      =  7Ascon.Pilot.SDK.GraphicLayerSample.Properties.Resources    Title � Title# Z  5      $	��500��5      $	��410��5       � 
ResizeMode$ NoResize=�5       � 	Resources 5       �� MergedDictionaries6   0Ascon.Pilot.Theme.Tools.SharedResourceDictionary   Source�� 5   
    5        5      $? 9/Ascon.Pilot.Theme;component/Themes/StandardControls.xaml?�61     5      $N H/Ascon.Pilot.Theme;component/Themes/SharedResources/SharedResources.xaml?�61     5      $ Resources.xaml?�61   %    5      .��5      �  � RowDefinitions 5   
   �� 5       $
��Auto �6   �� 5!      $
��Auto �6   �� 5"      $��* �6   .+�5%   
   �� -   5         ��Name$ ScannedSignaturePanel��5&      $��0��5%      .+�5(      �� ScannedSignaturePanelLabel��   DefaultFontSize#���   5)         DefaultTextBlockBottomMargin#���  5*        NormalForeground#���  5+         	ImagePath#��Z 5,      +   %Ascon.Pilot.Theme.Controls.ButtonEdit  5.      -   5        $��PathButtonEdit��5/       ScannedSignatureFilePathEdit��  
FolderIcon	   RightImageSource#	 [ 51      
 �� 
IsReadOnly
 .    KeepButtonsEnabled . $��Stretch=�55      ��50      �� *
FilePath+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ 	LostFocus=�   RightCommand 52      �� *SelectImageCommand+55      �� 57      SupportedFileTypes��   SupportedFiles#��Z 58        ReadOnlyForeground#���  59      $
��Wrap=�5:      5;      � 5=   
   $��1��6    � ColumnDefinitions 5>      �� 5?      $
��Auto �6#   �� 5@      $
��Auto �6#   .+�5C      �  5D      �� 5E      $
��Auto �6$   �� 5F      $
��Auto �6$   .+�5I      �� $��0��6      LinkToCorner#��Z 5J        DefaultTextBlockMargin#���  5K      #���  5L      #���   5M      � 5O      $��1��6    5P      �� 5Q      $
��Auto �6+   �� 5R      $
��Auto �6+   �� 5S      $
��Auto �6+   .+�5V      � $��0��6   $��Center=�5W       5Y      �� 5Z      $
��Auto �6,   �� 5[      $
��Auto �6,   �� 5\      $
��Auto �6,   .+�5_      	� -   5        $��LeftTopCornerButton��5d      $��0��5_   &    	� 	GroupName$ corner��5`      $��13��5b      $��13��5c       d� 	IsChecked 5a      �� *LeftTopCornerButtonChecked+5d      �� 5f      $��1��6%   $��4 0 0 0q�5g       �� Orientation$ 
Horizontal=�5h      $��75��5i      .+�5j      �� #��["�6(   5k      	� 5m      -   5        $��LeftBottomCornerButton��5r      $��2��5m   &   $ corner��5n      $��13��5p      $��13��5q       5o      �� *LeftBottomCornerButtonChecked+5r      5s      � 5u      $��1��6   $��Center=�5v       5x      �� 5y      $
��Auto �6,   �� 5z      $
��Auto �6,   �� 5{      $
��Auto �6,   .+�5~      �� $��0 0 0 4q�6%   $��0��5      $ Vertical=�5�      $��90��5�      .+�5�      �� #��["�6(   5�      �� 5�      $��0 0 0 0q�6%   $��1��5�      $ 
Horizontal=�5�      $��75��5�      $��90��5�      5�      �� 5�      $��0 4 0 0q�6%   $��2��5�      $ Vertical=�5�      $��90��5�      .+�5�      �� #��["�6(   5�      5�      � 5�      $��2��6   $��Center=�5�       5�      �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,   .+�5�      	� -   5        $��RightTopCornerButton��5�      $��0��5�   &   $ corner��5�       5�      �� *RightTopCornerButtonChecked+5�      �� 5�      $��4 0 0 0q�6%   $��1��5�      $ 
Horizontal=�5�      $��75��5�      .+�5�      �� #��["�6(   5�      	� 5�      -   5        $��RightBottomCornerButton��5�      $��2��5�   &   $ corner��5�       5�      �� * RightBottomCornerButtonChecked+5�      5�      5�      �� 5�      $��1��6   $ 
Horizontal=�5�      $��13,104,10,-37q�5�      .+�5�      �� -   5         ��Name$ TextBlockPage��5�       $��Center=�67   $
��Wrap=�6R      
PageNumber#��Z 5�      #���  5�      #���   5�      $��0,6,0,3q�6F   $��59��6W   $��32��6b   �� 5�      -   5         ��Name$ TxbPage��5�      $��	5,18,0,10q�5�      $��35��5�      ��5�      �� *
PageNumber+5�      5�      5�      � 5�      $��1��6    5�      �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   �� 5�      $��* �6$   .+�5�      �� $��0��6   $��	45 12 0 5q�5�      #���   5�         ImageOffsets#��Z 5�      #���  5�      �� 5�      $��1��6   $ 
Horizontal=�5�      $��45 0 0 0q�5�      .+�5�      �� $��Center=�6       OffsetX#��Z 5�      #���  5�       #���   5�      �� 5�      -	   5        $ 
TxbXOffset��5�      $��
5, 0, 0, 0q�5�      $��6��5�      $��35��5�      ��5�      �� *
XOffsetStr+5�      5�      �� 5�      $��2��6   $ 
Horizontal=�5�      $��	45 12 0 0q�5�      .+�5�      �� $��Center=�5�      #���  5�         OffsetY#��Z 5�      #���   5�      �� 5�      -
   5        $ 
TxbYOffset��5�      $��
5, 0, 0, 0q�5�      $��6��5�      $��35��5�      ��5�      �� *
YOffsetStr+5�      5�      �� 5�      $��3��6   $ 
Horizontal=�5�      $��	45 12 0 0q�5�      .+�5�      �� $��Center=�5�      #���  5�         Scale#��Z 5�      #���   5�      �� 5�      -   5        $ TxbScale��5�      $��
5, 0, 0, 0q�5�      $��6��5�      $��35��5�      ��5�      �� *Scale+5�      5�      �� 5     $��4��6   $ 
Horizontal=�5     $��	45 12 0 0q�5     .+�5     �� $��Center=�6       Angle#��Z 5      #���  5      #���   5      �� 5	     -   5        $ TxbAngle��5	     $��
5, 0, 0, 0q�5     $��6��5     $��35��5     ��5
     �� *Angle+5     5     � 5     $��5��6   $��	45 12 0 0q�5      5     �� 5     $
��Auto �6+   �� 5     $��* �6+   .+�5     �� -   5         ��Name$ StampCheckBox��5     $��0��5      5     �� *IncludeStamp+�� 5     $��Center=�6    $	��250��5      $��1��5      $
��Wrap=�5         AddStamp#��Z 5      #���  5      #���   5       5!     5"     5#     ]� 5%  
   $��2��6   $��Bottom=�5&       DefaultControlMargin#��[ 5'     $��Right=�5(     .+�5*     �� -   5         �� 	IsDefault . {�. $��OK��5,     5-     �� 5/     -   5          �� IsCancel  . {�. !   	CancelBtn#��Z! 51     $��8,0,0,0q�53     55     57     58     