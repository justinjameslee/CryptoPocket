   M S B A M L   `   `   `  ���� E  ADragablz, Version=0.0.3.203, Culture=neutral, PublicKeyToken=null5(clr-namespace:Dragablz;assembly=DragablzDragablz  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf G CCryptoPocket, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:CryptoPocketCryptoPocket    CryptoPocket.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     =dragablz(clr-namespace:Dragablz;assembly=Dragablz     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     ,localclr-namespace:CryptoPocket     V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullX TMaterialDesignThemes.MahApps, Version=0.0.11.0, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   -     � Title$  Crypto Pocket��$	��650��$	��950��   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�
�������  MaterialDesignFont#���   � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � DataContext ��  �� RelativeSource �� *self+.��� $	��650��$	��950��.+�� -   $��50��$	��Top=�.+�(  "MaterialDesignThemes.Wpf.ColorZone     Mode$ Accent=�$��50��.���  � ColumnDefinitions �� $
��200* ��� $
��545* ��� $
��100* �.+��� 	 �� Orientation$	 
Horizontal=�$��0��.+��� $��Crypto Pocket��$��Center=�$��16 0 0 0q�$��28(��� $��1��.+�� .+��� -     MaterialDesignRaisedButton#��[ $��50��$��1��$
��Auto��$��Right=�$��Center=�$��0,0,25,0q���Z� ��Z� .��� .+��� $��Guest��$��1��$��Right=�$��Center=�$��18(�$��0,0,35,0q�'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]
   Kind$
 Account=�$��25��$��25��$��Right=�$��Center=�$��50,0,0,0q��� $��2��.+�� .+��� #��[ $��50��$��50��$��Right=�$��0,0,50,0q���Z� ��Z� .��  $
 Minus=�$��30��$��30��$��Center=�$��Center=��� #��[ $��50��$��50��$��Right=���Z� ��Z� .��  $
 WindowClose=�$��30��$��30��$��Center=�$��Center=�)  #MaterialDesignThemes.Wpf.DialogHost  -   $��
AddTrading��.��� .+�"  CryptoPocket.AccountSettings  -     Name$ AccountSettings�� K� ZIndex$ 1��$��0,90,0,0q�$y�Hidden=�� $	��600��$��Bottom=�.+�   Dragablz.TabablzControl  -     Name$ 
HeaderTabs��$��2q�   FixedHeaderCount$ 4��$��2��$��Disabled=�+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�   InterTabController !	   Dragablz.InterTabController	  .@��� -    ��Name$ 
TabTrading��$��Disabled=����� $��Trading Dashboard��$��16(�$	��200�� �� TextAlignment$ Center=�.��� $	��950��$	��600��$	��950��$	��600��.+�#
  MaterialDesignThemes.Wpf.Card
  -   $��0��$	��Top=�$	��360��$	��540��$
��Left=�$��	10,10,0,0q�.��     CloseOnClickAway .    DialogContent �� $��16q�$	��250��.+��� $��Trading Portfolio��$��Center=�$��Center=�$��20(�$��
16,16,16,6q��� $��Custom Coin��$��Center=�$��Center=�$��14(�$��	16,0,16,6q��� $	 Vertical=�$	��250��.+��� -	    ��Name$ CoinComboBox��$��6q�$
��Auto��)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Coin Symbol��$��72�� & "MaterialDesignFloatingHintComboBox#��[ $��Bottom=� �� 
IsEditable .  �� Text ��  �� Path$ SelectedTextTwo� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=����� .R�$� � .+��� -
     MaterialDesignFlatButton#��[  �� 	IsDefault . $2��,materialDesign:DialogHost.CloseDialogCommand��$
��Left=�$��6,6,0,0q���O	 Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem	 ��5system&clr-namespace:System;assembly=mscorlib   True��.��ACCEPT�� #��[  �� IsCancel . $2��,materialDesign:DialogHost.CloseDialogCommand��$��Right=�$��0,6,6,0q���1&clr-namespace:System;assembly=mscorlibSystem	 ��5system&clr-namespace:System;assembly=mscorlib   	False��.��CANCEL.��� .+��� -   $��20q�$��10q�.���� -    ��Name$ CoinBox��$ �� IsSynchronizedWithCurrentItem$
 TrueY����� .R�  $ Depth1=�.��� $
��Auto��$��90��.+�� $
��Left=�$	��128��.+��� 
��� ���
��� ��� & "MaterialDesignFloatingActionButton#��[   � ToolTip$(  "MaterialDesignFloatingActionButton��$
��Left=�$��	15,0,20,0q�$��55��$��55��$��18(�$	��ETH��  $
 TrendingDown=�$��40��$��40��$��Right=�$��Center=�$��
0,25,10,25q�� $��
54,0,392,0q�$��Center=�$��Center=�� $��Center=�$��Center=�! � RowDefinitions! �� $	��50* ��� $	��50* �.+��� $��0��$��ETH/USD: $583.03��$��Center=�$��Center=�$��18(�$��0,7,24,5q��� $��1��  MaterialDesignDarkSeparator#��[ $��0,0,0,50q�$��Center=�$��Center=��� $��1��$��ETH/BTC: 0.07791070��$��Center=�$��Center=�$��18(�$��	-6,17,6,6q�� $��Right=�! �� $	��40* ��� $	��60* �.+��� " ��Name$" ToggleCoinText��$��Portfolio Value��$
��Left=�$��Center=�$��	25,12,0,8q�d� -    $  MaterialDesignActionToggleButton#��[ $+  %MaterialDesignActionLightToggleButton��$��Right=�$��0,5,5,0q�$��28��$��28����  $
 AccountCardDetails=�$��15��$��15��1  +MaterialDesignThemes.Wpf.ToggleButtonAssist#   	OnContent#   $
 ChartLineVariant=��� $" PortfolioCoin��$ J� Opacity$	$ 100Q�$��	ETH: 0.52��$��2��$��
0,-10,0,20q�$��Center=�$��Center=�$��16(��� $" PortfolioCoinV��$	$ 100Q�$��$226.62 USD��$��2��$��Center=�$��Center=�$��0,10,0,0q�$��16(��� $" 
txt24Hours��$$ 0Q�$��24 Hours��$��1��$��Center=�$��Center=�$��
25,4,73,34q�% J� RenderTransformOrigin$% 
0.846,0.75&��� $" txt7Days��$$ 0Q�$��7 Days��$��1��$��Center=�$��Center=�$��39,26,73,12q�  &  Name$& Icon24Hours��$$ 0Q�$��1��$
 TrendingDown=�$��16��$��15��$��Center=�$��Center=�$��125,25,10,13q�$% 0.313,1.097&��� $" txt24HoursV��$$ 0Q�$��$20.62��$��1��$��Center=�$��Center=�$��
82,4,30,34q��� $" 	txt7DaysV��$$ 0Q�$��$2.12��$��1��$��Center=�$��Center=�$��89,26,30,12q�$% -0.551,0.959&�  $& 	Icon7Days��$$ 0Q�$��1��$
 
TrendingUp=�$��16��$��15��$��Center=�$��Center=�$��125,4,10,34q�$% 0.313,1.097&��� -   ' ��Name$' CustomCoinButton��$$ 0Q�?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid$(   SelectiveScrollingOrientation$
( None=�
���2͚�
���2͚�)   OpenDialogCommand#��Z)  * &MaterialDesignFloatingActionDarkButton#��[ $,  &MaterialDesignFloatingActionDarkButton��$��Bottom=�$��Right=�$��	0,0,55,30q�.��  $

 Plus=�$��24��$��24��
  $��0��$��Bottom=�$	��180��$	��920��$
��Left=�$��	10,0,0,33q�$��20q�.���  �� $	��240 ��� $	��210 ��� $	��250 ��� $
��Auto �.+�
  
��� ���$��0�� !	 PrimaryHueDarkForegroundBrush#��� 	 $��0q�$	��240��$
��Left=�$��Center=�.��� ! �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� .+�  $ Accent=�$��0q�.���� $	 
Horizontal=�.+�  $
 CashMultiple=�$��	20,9,16,8q�$��Center=�$	��Top=�$��50��$��35���� $��0��$��
0,16,16,14q� #
 MaterialDesignHeadlineTextBlock#��[
 $ Center=�
Investment  $ 
PrimaryMid=�$	��Top=�$��0q�.���� .+��� $��2��$
��16,8q�$��Center=�$��Center=�   MaterialDesignBody2TextBlock#��[ $��24(�$��$32,000 AUD��
  
��� ���$��1��$��10,0,0,0q�#��� 	 $��0q�$	��200��$
��Left=�$��Center=�.��� ! �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� .+�  $ Accent=�$��0q�.���� $	 
Horizontal=�.+�  $
 
TrendingUp=�$��	20,9,16,8q�$��Center=�$��Center=�$��50��$��35���� $��0��$��
0,16,16,14q�#��[
 $ Center=�Profit  $ 
PrimaryMid=�$	��Top=�$��0q�.���� .+��� $��2��$
��16,8q�$��Center=�$��Center=�#��[ $��24(�$��$12,000 AUD��
  
��� ���$��2��$��10,0,0,0q�#��� 	 $��0q�$	��200��$
��Left=�$��Center=�.��� ! �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� .+�  $ Accent=�$��0q�.���� $	 
Horizontal=�.+�  $
 Coins=�$��	20,9,16,8q�$��Center=�$��Center=�$��50��$��35���� $��0��$��
0,16,16,14q�#��[
 $ Center=�Coins  $ 
PrimaryMid=�$	��Top=�$��0q�.���� .+��� $��2��$
��16,8q�$��Center=�$��Center=�#��[ $��24(�$��32��� $��3��$��Center=�$��Center=�.+��� $��Trading Portfolio��$��Center=�$��Center=�$ Center=�$��20(�$��0,0,5,75q��� #��Z) 
���2͚�
���2͚�#��[ $(  "MaterialDesignFloatingActionButton��$
��Left=�$��0,40,0,0q����� * �� ElementName$* 
AddTrading��.��  $

 Plus=�$��24��$��24������ $��16q�$	��250��.+��� $��Trading Portfolio��$��Center=�$��Center=�$��20(�$��
16,16,16,6q��� $��Add Coin��$��Center=�$��Center=�$��14(�$��	16,0,16,6q��� $	 Vertical=�$	��250��.+��� -   $ TradComboBox��$��6,6,6,6q�$
��Auto��$ Coin Symbol��$��72��#��[ $��Bottom=� .  �� $ SelectedTextTwo�$ TwoWay=�$ PropertyChanged=����� .R�$� (  "System.Windows.Controls.DatePicker  $
��Auto��$��6,16,6,6q�$ Time Purchased�� ( $MaterialDesignFloatingHintDatePicker#��[ �� $��6,6,6,6q�$
��Auto��$ Purchase Price�� % !MaterialDesignFloatingHintTextBox#��[ .���� $��6,6,6,6q�$
��Auto��$ Quantity Purchased��#��[ .��� .+��� -   #��[  . $2��,materialDesign:DialogHost.CloseDialogCommand��$
��Left=�$��6,6,0,0q���1&clr-namespace:System;assembly=mscorlibSystem	 ��5system&clr-namespace:System;assembly=mscorlib   True��.��ACCEPT�� #��[  . $2��,materialDesign:DialogHost.CloseDialogCommand��$��Right=�$��0,6,6,0q���1&clr-namespace:System;assembly=mscorlibSystem	 ��5system&clr-namespace:System;assembly=mscorlib   	False��.��CANCEL�� #��Z) 
���r���
���r���#��[ $(  "MaterialDesignFloatingActionButton��$
��Left=�$��
100,40,0,0q����� $* 
AddTrading��.��  $
 Minus=�$��24��$��24������ $��16q�$	��250��.+��� $��Trading Portfolio��$��Center=�$��Center=�$��20(�$��
16,16,16,6q��� $��Remove Coin��$��Center=�$��Center=�$��14(�$��	16,0,16,6q��� $	 Vertical=�$	��250��.+��� -   $ TradRemoveComboBox��$��6,6,6,6q�$
��Auto��$ Coin Symbol��$��72��#��[ $��Bottom=� .  �� $ SelectedTextTwo�$ TwoWay=�$ PropertyChanged=����� .R�$�   $
��Auto��$��6,16,6,6q�$ 	Time Sold��#��[ �� $��6,6,6,6q�$
��Auto��$ 
Sale Price��#��[ .���� $��6,6,6,6q�$
��Auto��$ Quantity Sold��#��[ .��� .+��� -   #��[  . $2��,materialDesign:DialogHost.CloseDialogCommand��$
��Left=�$��6,6,0,0q���1&clr-namespace:System;assembly=mscorlibSystem	 ��5system&clr-namespace:System;assembly=mscorlib   True��.��ACCEPT�� #��[  . $2��,materialDesign:DialogHost.CloseDialogCommand��$��Right=�$��0,6,6,0q���1&clr-namespace:System;assembly=mscorlibSystem	 ��5system&clr-namespace:System;assembly=mscorlib   	False��.��CANCEL
  $��1��$��16q�$	��Top=�$	��360��$	��375��$��Right=�$��	0,10,10,0q�.��� .+�  LiveCharts.Wpf.PieChart  -   &   LiveCharts.Wpf.Charts.Base.Chart+   LegendLocation$+ Bottom=�,   	Hoverable, . $��0,0,0,0q�-   Series-   LiveCharts.Wpf.PieSeries    LiveCharts.Wpf.Series.   Title$. Bitcoin��/   Values;  5LiveCharts.Wpf.Converters.NumericChartValuesConverter$/ 3 0   
DataLabels0 .  1   
LabelPoint1 �� *
PointLabel+  $. Ethereum��$/ 4 0 .  1 �� *
PointLabel+  $. Litecoin��$/ 6 0 .  1 �� *
PointLabel+  $. Bitcoin Gold��$/ 2 0 .  1 �� *
PointLabel+  $. Other��$/ 1 0 .  1 �� *
PointLabel+�� $��Disabled=�2 �� 
IsSelected2 . ���� $��Mining Dashboard��$��16(�$	��200��$ Center=�.��� $	��950��$	��600��$	��950��$	��600��.+�
  -   $��0��$	��Top=�$	��360��$	��540��$
��Left=�$��	10,10,0,0q�.��   .  �� $��16q�$	��250��.+��� $��Mining Portfolio��$��Center=�$��Center=�$��20(�$��
16,16,16,6q��� $��Custom Worker��$��Center=�$��Center=�$��14(�$��	16,0,16,6q��� $	 Vertical=�$	��250��.+��� -   $ WorkerComboBox��$��6q�$
��Auto��$ Worker��$��72��#��[ $��Bottom=� .  �� $ SelectedTextTwo�$ TwoWay=�$ PropertyChanged=����� .R�$� � .+��� -   #��[  . $2��,materialDesign:DialogHost.CloseDialogCommand��$
��Left=�$��6,6,0,0q���1&clr-namespace:System;assembly=mscorlibSystem	 ��5system&clr-namespace:System;assembly=mscorlib   True��.��ACCEPT�� #��[  . $2��,materialDesign:DialogHost.CloseDialogCommand��$��Right=�$��0,6,6,0q���1&clr-namespace:System;assembly=mscorlibSystem	 ��5system&clr-namespace:System;assembly=mscorlib   	False��.��CANCEL.��� .+��� -   $��20q�$��10q�.���� -   $ 	WorkerBox��$
 TrueY����� .R�  $ Depth1=�.��� $
��Auto��$��90��.+�� $
��Left=�$	��128��.+��� $��10,10,-59,50q�.+��� $��GPU-01��$��18(��� �� $��Profitability��$��16(�$��10,40,343,32q��� $��USD/DAY: $12.62��$��10,63,281,11q�$% -0.173,1.625&��� $��Hashrate��$��16(�$��195,10,174,60q��� $��4100H/S��$��195,35,141,38q��� $��Up Time��$��16(�$��299,10,76,60q�$% 
0.459,0.65&��� $��1 Days 14 Hours��$��299,35,10,38q��� $��Online��$��14(����	#DD338B00$��195,62,174.333,10q��� $ ��29 September 2015 12:50 AM��$��0,63,11.333,10q�$% -0.317,0.882&�$��12(�$��Right=�d� -   #��[ $+  %MaterialDesignActionLightToggleButton��$��Right=�$��0,40,252,26q�$��24��$��24������ $	��USD��$��8(�$��0q�# �� $	��AUD��$��8(�$��0q��� -   $' CustomWorkerButton��$$ 0Q�$
( None=�
���2͚�
���2͚�#��Z) #��[ $,  &MaterialDesignFloatingActionDarkButton��$��Bottom=�$��Right=�$��	0,0,55,30q�.��  $

 Plus=�$��24��$��24��
  $��0��$��Bottom=�$	��180��$	��920��$
��Left=�$��	10,0,0,33q�$��20q�.���  �� $	��240 ��� $	��210 ��� $	��250 ��� $
��Auto �.+�
  
��� ���$��0��#��� 	 $��0q�$	��240��$
��Left=�$��Center=�.��� ! �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� .+�  $ Accent=�$��0q�.���� $	 
Horizontal=�.+�  $
 CashMultiple=�$��	20,9,16,8q�$��Center=�$	��Top=�$��50��$��35���� $��0��$��
0,16,16,14q�#��[
 $ Center=�
Investment  $ 
PrimaryMid=�$	��Top=�$��0q�.���� .+��� $��2��$
��16,8q�$��Center=�$��Center=�#��[ $��24(�$��$12,000 AUD��
  
��� ���$��1��$��10,0,0,0q�#��� 	 $��0q�$	��200��$
��Left=�$��Center=�.��� ! �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� .+�  $ Accent=�$��0q�.���� $	 
Horizontal=�.+�  $
 
TrendingUp=�$��	20,9,16,8q�$��Center=�$��Center=�$��50��$��35���� $��0��$��
0,16,16,14q�#��[
 $ Center=�Daily  $ 
PrimaryMid=�$	��Top=�$��0q�.���� .+��� $��2��$
��16,8q�$��Center=�$��Center=�#��[ $��24(�$��
$23.56 AUD��
  
��� ���$��2��$��10,0,0,0q�#��� 	 $��0q�$	��200��$
��Left=�$��Center=�.��� ! �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� .+�  $ Accent=�$��0q�.���� $	 
Horizontal=�.+�  $
 Worker=�$��	20,9,16,8q�$��Center=�$��Center=�$��50��$��35���� $��0��$��
0,16,16,14q�#��[
 $ Center=�Active  $ 
PrimaryMid=�$	��Top=�$��0q�.���� .+��� $��2��$
��16,8q�$��Center=�$��Center=�#��[ $��24(�$	��4/6��� $��3��$��Center=�$��Center=�.+��� $��Mining Portfolio��$��Center=�$��Center=�$ Center=�$��20(�$��0,0,5,75q��� 
���2͚�
���2͚�#��[ $(  "MaterialDesignFloatingActionButton��$
��Left=�$��0,40,0,0q�.��  $

 Plus=�$��24��$��24���� 
���r���
���r���#��[ $(  "MaterialDesignFloatingActionButton��$
��Left=�$��
100,40,0,0q�.��  $
 Minus=�$��24��$��24��
  $��1��$��16q�$	��Top=�$	��360��$	��375��$��Right=�$��	0,10,10,0q�.��� .+�  -   $+ Bottom=�, . $��0,0,0,0q�-   $. GPU-01��$/ 10 0 .  1 �� *
PointLabel+  $. ASIC-01��$/ 8 0 .  1 �� *
PointLabel+  $. ASIC-02��$/ 4 0 .  1 �� *
PointLabel+  $. GPU-02��$/ 4 0 .  1 �� *
PointLabel+  $. Other��$/ 2 0 .  1 �� *
PointLabel+�� $��Disabled=����� $��Monthly Reports��$��16(�$	��200��$ Center=�.��� $	��950��$	��600��! �� $	��125 ��� $
��Auto �.+�
  $��0��$��
10,10,0,10q�$	��110��$	��280��$��10q�$
��Left=�.���� .+��  �� $	��120 ��� $	��120 �.+��� $��0��.+��� $��0��$
��Year��$��22(�$
��Left=�$��5,0,0,0q��� $��0��$
 2018��$	��100��$��5q�$��Bottom=�$
��Left=�$��14(�3 �� SelectedValue3 �� *SelectedValueOne+4 �� SelectedItem4 �� $ SelectedValueOne�$ TwoWay=�$ PropertyChanged=����� .R�$� .@��� $
��2018���� $
��2019���� $��1��.+��� $��0��$��Month��$��22(�$
��Left=�$��5,0,0,0q��� $��0��$	 May��$	��100��$��5q�$��Bottom=�$
��Left=�$��14(�3 �� *SelectedValueOne+4 �� $ SelectedValueOne�$ TwoWay=�$ PropertyChanged=����� .R�$� .@��� $��January���� $��Febuary���� $��March���� $��April���� $	��May���� $
��June���� $
��July���� $��August���� $��	September���� $��October���� $��November���� $��December��
  $��0��$	��635��$	��110��$��
0,10,10,10q�$��10q�$��Right=�.��� .+��� .+��� $#��Monthly Report for May - 2018��$��28(�$
��Left=�$��Center=�$��0,15,0,0q��� 
  $��1��$	��910��$	��409��$��Stretch=�.��� .+�� ! �� $
��250* ��� $
��Auto �.+�� $��0�� �� $
��225* ��� $
��150* ��� $	��20* ��� $
��250* ��� $
��150* �.+��� $��0��$��
0,25,25,25q�$	��135��$��Center=�.+��� $��Revenue��$��34(��� �� $��0��$��	45,25,0,0q��� $��1��$��	0,25,20,0q��� $
��Left=�$��45,90,25,25q�$��0��.+��� $��Trading Income��$��24(��� $��Right=�$��25,90,25,25q�$��1��.+��� $��$8,000��$��24(��� $��0��$��
45,145,0,0q��� $��1��$��
0,145,20,0q��� $
��Left=�$��45,150,25,25q�$��0��.+��� $��Mining Income��$��24(��� $��Right=�$��25,150,25,25q�$��1��.+��� $��$12,000��$��24(��� $��3��$��
0,25,30,25q�$	��145��.+��� $��Expenses��$��34(��� �� $��3��$��	50,25,0,0q��� $��4��$��	0,25,45,0q��� $
��Left=�$��53,90,25,25q�$��3��.+��� $��Electricity��$��24(��� $��Right=�$��25,90,50,25q�$��4��.+��� $��$2,000��$��24(��� $��3��$��
50,145,0,0q��� $��4��$��
0,145,45,0q��� $
��Left=�$��53,150,25,25q�$��3��.+��� $��Transaction Fees��$��24(��� $��Right=�$��25,150,50,25q�$��4��.+��� $	��N/A��$��24(�� $��1��$��25q�.+��� $��0,0,0,35q�.+�
  
��� ���$��2��$��0,0,0,0q�#��� 	 $��0q�$	��200��$
��Left=�$��Center=�.��� ! �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� .+�  $ Accent=�$��0q�.���� $	 
Horizontal=�.+��� $��0��$��27,16,16,14q�#��[
 $ Center=�$��Center=�Total Revenue  $ 
PrimaryMid=�$	��Top=�$��0q�.���� .+��� $��2��$
��16,8q�$��Center=�$��Center=�#��[ $��24(�$��$20,000���� $��	185,0,0,0q�.+�
  
��� ���$��2��$��20,0,0,0q�#��� 	 $��0q�$	��220��$
��Left=�$��Center=�.��� ! �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� .+�  $ Accent=�$��0q�.���� $	 
Horizontal=�.+��� $��0��$��22,16,16,14q�#��[
 $ Center=�$��Center=�Total Expenses  $ 
PrimaryMid=�$	��Top=�$��0q�.���� .+��� $��2��$
��16,8q�$��Center=�$��Center=�#��[ $��24(�$��$2,000���� $��	0,0,215,0q�$��Right=�.+�
  
��� ���$��2��$��20,0,0,0q�#��� 	 $��0q�$	��215��$
��Left=�$��Center=�.��� ! �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� .+�  $ Accent=�$��0q�.���� $	 
Horizontal=�.+��� $��0��$��45,16,16,14q�#��[
 $ Center=�$��Center=�
Net Profit  $ 
PrimaryMid=�$	��Top=�$��0q�.���� .+��� $��2��$
��16,8q�$��Center=�$��Center=�#��[ $��24(�$��$18,000���� $��0,0,0,0q�$��Right=�.+�
  
��� ���$��2��$��30,0,0,0q�#��� 	 $��0q�$	��220��$
��Left=�$��Center=�.��� ! �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� .+�  $ Accent=�$��0q�.���� $	 
Horizontal=�.+��� $��0��$��38,16,16,14q�#��[
 $ Center=�$��Center=�Total Profit  $ 
PrimaryMid=�$	��Top=�$��0q�.���� .+��� $��2��$
��16,8q�$��Center=�$��Center=�#��[ $��24(�$��$22,000��