   M S B A M L   `   `   `  ����B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp2WpfApp2  5   	   'clr-namespace:Converters
Converters  5   	       WpfApp2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp2     5   	   'yyclr-namespace:Converters     5   	   -   5          � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)    J� CommandBindings 5
      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$ ApplicationCommands.Open��5      5        5   
   -   5        $ ApplicationCommands.Save��5      5        5   
   -   5        $  ApplicationCommands.Delete��5      5        5   
   -   5           WpfApp2.CustomCommands   AddCustomCommand# Z 5      5       � 	Resources 5         key_view_DataCollection �� Source  key_view_DataOnGrid*   $Converters.DataCollection_Converter1 ! key_datacollection_converter1*   $Converters.DataCollection_Converter2 ! key_datacollection_converter2!   Converters.MaxMin_Converter  key_maxmin_converter   Converters.Max_Converter  key_max_converter  key_list_template �� Orientation �� 	Converter��%�  &	        &	 7     &	 n     &	 �     &	 �     &	 �     &	 �     7  7  �� 5   
   -   5         5      �� 5   ?   �� 5   
   -   5         5      �� 5   ;     5    
   6'     5!   
   6'     5"   
   6     5#   
   6   �� 5$   
   .R�5%      �� $ Vertical=�6   .+�5&      �� ��6   �� 8   �� 5'      ��6   �� 8  5(      5)      5*      .��5+      �  � RowDefinitions 5,   
   �� 5-      $��1* �6   �� 5.      $��3* �6   �� 5/      $��2* �6   �� 50      $��2* �6   	 � ColumnDefinitions	 52   
   �� 53      $��2* �6   �� 54      $��2* �6   �� 55      $��1* �6   .+�57   
   �� $(��"All elements from V4MainCollection��6   $��Stretch=�58      $��10,3,0,0q�6+   $��1��6=   $
��Wrap=�6J   $	��Top=�6^   �� 59   
   -   5        $��listBox_Main��59   4   $��1��6   $��10,30,10,10q�6   ��6H   �� �� 5;   
   $��Type - V4DataCollection��6   $��Stretch=�5<      $��10,3,0,0q�6+   $��2��6=   $
��Wrap=�6J   $	��Top=�6^   �� 5=   
   -	   5        $��listBox_DataCollection��5=   4   $��2��6   $��10,30,10,10q�6   $
 �� IsSynchronizedWithCurrentItem$

 TrueY�5?      ��5>      ��  �� Source# [  5?      �� 5A   
   $��Type - V4DataOnGrid��6   $��Stretch=�5B      $��10,3,0,0q�6+   $��3��6=   $
��Wrap=�6J   $	��Top=�6^   �� 5C   
   -
   5        $��listBox_DataOnGrid��5C   4   $��3��6   $��10,30,10,10q�6   $

 TrueY�5E      ��5D      �� # [ 5E      �� 5G   
   $&�� All elements form DataCollection��6   $��Stretch=�5H      $��10,3,0,0q�6+   $��2��6=   $��1��6J   $
��Wrap=�6Z   $	��Top=�6n   �� 5I   
   -   5        $��listBox_details��5I   D   $��1��6   $��2��6"   $��10,30,10,10q�6/   #��[ 5K      ��5J      �� # [   �� Path$ CurrentItem�5K      o� 5N   
   $��30��6   $��10,10,10,10q�6   $��Center=�60   .@�5O      m� $
��File��6   .@�5P      m� -   5        $	��New��5P      6(   m� 5Q      $
��Open��6    m� Command$
 Open��6)   m� 5R      $
��Save��6   $
 Save��6)   5S      m� 5T      $
��Edit��6   .@�5U      m� -   5        $��Add Defaults��5U      61   m� 5V      -   5        $"��Add Default V4DataCollection��5V      6A   m� 5W      -   5        $��Add Default V4DataOnGrid��5W      6=   m� 5X      -   5        $��Add Element from File��5X      6:   m� 5Y      $��Remove��6   $ Delete��6+   m� 5Z      $ ��AddCustom V4DataCollection��6   # Z 6?   5[      5\      �� 5_   
   $#��Max abs in the MainCollection��6   $��Stretch=�5`      $��10,3,0,0q�6+   $��1��6=   $��1��6J   $
��Wrap=�6Z   $	��Top=�6n   �� 5a   
   $��1��6   $��Stretch=�6$   $��10,20,20,20q�6B   $��1��6W   $
��Wrap=�6d   $��Center=�6x   ��5b      �� $ max_abs�# [ �� 5e   
   $4��.Min and Max in the selected DataOnGrid element��6   $��Stretch=�5f      $��10,3,0,0q�6+   $��3��6=   $��1��6J   $
��Wrap=�6Z   $	��Top=�6n   �� 5g   
   $��1��6   $��Stretch=�6$   $��10,20,10,10q�6B   $��3��6W   $
��Wrap=�6d   $��Center=�6x   ��5h      �� # [ $ CurrentItem�# [ � 5k   
   -   5        $��grid_AddCustom��5k      $��1��6%   $��2��62   $��3,3,3,3q�6B   .+�5l      �� -   5        $��stackPanel_AddCustom��5l      $ Vertical=�65   $��5,5,5,5q�6L   $��2��6]   $��1��6m   .+�5m      �� $��Information:��6   �� 5n      $	��125��6   $
��Left=�6&   ��5o      �� *info+ �� ValidatesOnDataErrors$
 True�� �� UpdateSourceTrigger$ PropertyChanged=��� 5p      $��Number of elements:��6   �� 5q      $	��125��6   $
��Left=�6&   ��5r      �� *num+$
 True��$ PropertyChanged=��� 5s      $��	minValue:��6   �� 5t      $	��125��6   $
��Left=�6&   ��5u      �� *
minValue+$
 True���� 5v      $��	maxValue:��6   �� 5w      $	��125��6   $
��Left=�6&   ��5x      �� *
maxValue+$
 True��5y      5z      �� 5}      $
��Open��6   $
��Open��6   $��1��5~      $
��Left=�6!   $��5,15,0,0q�6<   $	��Top=�6N   $��22��6f   $��60��6r   �� 5�   
   $
��Save��6   $
��Save��6    $��1��5�      $
��Left=�6!   $��	75,15,0,0q�6<   $	��Top=�6O   $��22��6g   $��60��6s   �� 5�   
   $��Remove��6   $��Delete��6"   $��1��5�      $
��Left=�6!   $��
145,15,0,0q�6<   $	��Top=�6P   $��22��6h   $��60��6t   �� 5�   
   $��
Add Custom��6   #��Z 6&   $��2��5�      $��Center=�6!   $��0,15,0,0q�6>   $	��Top=�6P   $��22��6h   $��80��6t   5�      5�      