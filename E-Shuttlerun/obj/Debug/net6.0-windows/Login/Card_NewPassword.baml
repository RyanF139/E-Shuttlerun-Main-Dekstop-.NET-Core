   M S B A M L   `   `   `  ????G  CE-Shuttlerun, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:E_Shuttlerun.LoginE_Shuttlerun.Login  5      )    #E_Shuttlerun.Login.Card_NewPassword   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:E_Shuttlerun.Login     5      c _MaterialDesignThemes.MahApps, Version=0.2.2.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5         MaterialDesignBody#???   5	      $??Regular"?5
      $??13(?5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=?5         TextRenderingMode$
 Auto=?5        MaterialDesignFont#???  5      .??5      7  1MaterialDesignThemes.Wpf.Transitions.Transitioner   ?? SelectedIndex$ 0??6"   !   AutoApplyTransitionOrigins$
 True??64   .@?5   
   <  6MaterialDesignThemes.Wpf.Transitions.TransitionerSlide  $
}?True??5      C  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase   OpeningEffects 5      ;  5MaterialDesignThemes.Wpf.Transitions.TransitionEffect  5         Kind$ ExpandIn=?5         Duration$ 0:0:0.8h?5      .??5      ? $??10q?6   .+?5      #  MaterialDesignThemes.Wpf.Card  $	??600??6&   $	??410??63   ???#045BC7   UniformCornerRadius$ 30Q?5      ?? 5      $	??154??6   $	??154??6*   $??
128,41,0,0q?66   $	??Top=?5      $
??Left=?6-   .+?5      ?? $??/Asset/Icon User.png??6   5       ?? 5"      $??45??6   $	??310??6)   $??
50,201,0,0q?65   $	??Top=?5#      $
??Left=?6-   .+?5$        $??45??6*   $	??310??66   $ 10Q?5%   "   ???#FFFFFF+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth2=?5'   "   .??5(      ? .+?5)      ?? $$??/Asset/Icon Password Field.png??6$   $??25??5*      $??25??6$   $??11,0,0,0q?6/   $??Center=?5+      $
??Left=?63   G? 5,      -   5        $??textCurrentPassword??5,   -   $
??auto??5-   -   $	??250??6;   $??	46,10,0,7q?6G   $??16(?5.   -   )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 Current Password??5/   -   
   HelperTextFontSize$
 20Q?50   -   51      52      53      ?? 55      $??45??6   $	??310??6)   $??
50,271,0,0q?65   $	??Top=?56      $
??Left=?6-   .+?57        $??45??6*   $	??310??66   $ 10Q?58   "   ???#FFFFFF$ Depth2=?5:   "   .??5;      ? .+?5<      ?? $$??/Asset/Icon Password Field.png??6$   $??25??5=      $??25??6$   $??11,0,0,0q?6/   $??Center=?5>      $
??Left=?63   G? 5?      -   5        $??NewPassword??5?   -   $
??auto??5@   -   $	??250??6;   $??	46,10,0,7q?6G   $??16(?5A   -   $	 New Password??5B   -   $
 20Q?5C   -   5D      5E      5F      ?? 5H      $??45??6   $	??310??6)   $??
50,341,0,0q?65   $	??Top=?5I      $
??Left=?6-   .+?5J        $??45??6*   $	??310??66   $ 10Q?5K   "   ???#FFFFFF$ Depth2=?5M   "   .??5N      ? .+?5O      ?? $$??/Asset/Icon Password Field.png??6$   $??25??5P      $??25??6$   $??11,0,0,0q?6/   $??Center=?5Q      $
??Left=?63   G? 5R      -   5        $??ConfirmNewPassword??5R   -   $
??auto??5S   -   $	??250??6;   $??	46,10,0,7q?6G   $??16(?5T   -   $	 Confirm New Password??5U   -   $
 20Q?5V   -   5W      5X      5Y      ? 5[      $??40??6   $	??310??6#   $??
50,432,0,0q?6/   $	??Top=?5\      $
??Left=?6/   .+?5]      ?? -   5         ??Name$ btn_UpdatePassword??5]       ! MaterialDesignPaperDarkButton#??[ 5_      +	  %MaterialDesignThemes.Wpf.ButtonAssist 	  CornerRadius$ 10??5`      $??40??5a      $	??310??6)   
?????? ???#044DB5$??Update Password / Login??5c      5d      5e      5f      5g      5h      