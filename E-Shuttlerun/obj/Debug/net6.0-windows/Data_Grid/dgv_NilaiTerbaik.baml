   M S B A M L   `   `   `  ????G  CE-Shuttlerun, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:E_Shuttlerun.Data_GridE_Shuttlerun.Data_Grid  5      -    'E_Shuttlerun.Data_Grid.dgv_NilaiTerbaik   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:E_Shuttlerun.Data_Grid     5      .??5	      ?   ? RowDefinitions  5
   
   ?? 5      $	??220 ?6   .+?5   
   ? $??0??6   $??10q?6   $??Stretch=?6(   $	??Top=?6F   .+?5      &   System.Windows.Controls.DataGrid  -   5          Name$ dgvNilaiTerbaik??5      
??????    AutoGenerateColumns$ False??6o      CanUserAddRows$ False??6?      CanUserSortColumns$ False??6?   $
??True??6?   $
??Auto=?6?   ??60   ?? *DaftarTerbaik+ ? 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader ?? BasedOn ?? 
TargetType ?? Property	 ?? Value??%?   '        7 ?? 5      8   "  6U   .?5      ??  ? ??	 ?#37474f67   ?? 5       ? ??
	 ?????67   5      5      
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ RANGKING??6*      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6V      
IsReadOnly$
 True??6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   ?? *NO+   ElementStyle 5      ?? 5      " ??6$   .?5   "   ??  ? ??$	 Center=?6P   5      5        5      $ NO TEST??6)   $ 3* 6V   $
 True??6a    6:   ?? *	NO_TEST+ 5      ?? 5      " ??6$   .?5    "   ??  ? ??$	 Center=?6P   5!      5#        5$      $
 NAMA??6)   $ 3* 6P   $
 True??6[    67   ?? *NAMA+ 5%      ?? 5&      " ??6$   .?5'   "   ??  ? ??$	 Center=?6P   5(      5*        5+      $ HASIL??6)   $ 3* 6R   $
 True??6]    68   ?? *HASIL+ 5,      ?? 5-      " ??6$   .?5.   "   ??  ? ??$	 Center=?6P   5/      51        52      $ TESTOR??6)   $ 3* 6T   $
 True??6_    69   ?? *TESTOR+ 53      ?? 54      " ??6$   .?55   "   ??  ? ??$	 Center=?6P   56      58      5:      5;      5<      5=      