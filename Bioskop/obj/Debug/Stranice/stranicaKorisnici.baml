   M S B A M L   `   `   `  ����B  >Bioskop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Bioskop.StraniceBioskop.Stranice  5      (    "Bioskop.Stranice.stranicaKorisnici   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Bioskop.Stranice     5      $	��420��5	      $	��720��6   $	��420��5
      $	��720��6     N� Title$  	Korisnici��5       N� 
Background �	#FFD1D2FF.��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��DataGrid��5         AutoGenerateColumns .  $��10, 215, 10, 10q�6?      AlternatingRowBackground
 ����� �� AlternationCount$ 2��6�   ���	#FF5D49EB   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Korisnicko Ime��6G      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6_   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *korisnickoIme+6_     5      $ Email��6?   $ * 6N    6%   �� *email+6N     5      $ Lozinka��6A   $ * 6R    6%   �� *	lozinka+6R     5      $	 Ime��6=   $ * 6J    6%   �� *ime+6J     5      $ Prezime��6A   $ * 6R    6%   �� *	prezime+6R     5      $ ID��6E   $ * 6Q    6%   �� *idKorisnika+6Q   5      � 5   
   $��Center=�6   $	��200��6,   $	��700��69   $	��Top=�6E   .+�5      �� $��	KORISNICI��6   $
��Left=�6*   $��	115,4,0,0q�6E   $��16(�6X   $	��Top=�6f   $��95��6~   ���	#FF5D49EB$
��Bold"�6�   ���	#FF4130BD6�   �� 5      $��Korisnicko Ime��6   $
��Left=�6;   $��215,159,0,0q�6V   $��14(�6m   $	��Top=�6�   �� 5      $	��Ime��6   $
��Left=�6.   $��
215,36,0,0q�6I   $��14(�6^   $	��Top=�6r   �� 5      $��Email��6   $
��Left=�6)   $��
215,96,0,0q�6D   $��14(�6Y   $	��Top=�6m   �� 5      $��Lozinka��6   $
��Left=�65   $��215,124,0,0q�6P   $��14(�6e   $	��Top=�6y   �� 5      $��Prezime��6   $
��Left=�65   $��
215,66,0,0q�6P   $��14(�6e   $	��Top=�6y   �� 5       -   5        	 ��Name$	 txtbxKorisnickoIme��5       $
��Left=�6<   $��
10,163,0,0q�6W   
 �� TextWrapping$

 Wrap=�6m   $	��Top=�6�   $	��200��6�   $��20��6�   ���	#FF5D49EB6�   �� 5!      -   5        $	 txtbxIme��5!      $
��Left=�60   $��	10,40,0,0q�6K   $

 Wrap=�6`   $	��Top=�6t   $	��200��6�   $��20��6�   ���	#FF5D49EB6�   �� 5"      -   5        $	 txtbxPrezime��5"      $
��Left=�67   $��	10,70,0,0q�6R   $

 Wrap=�6g   $	��Top=�6{   $	��200��6�   $��20��6�   ���	#FF5D49EB6�   �� 5#      -   5        $	 
txtbxEmail��5#      $
��Left=�6,   $��
10,100,0,0q�6G   $

 Wrap=�6\   $	��Top=�6p   $	��200��6�   $��20��6�   ���	#FF5D49EB6�   �� 5$      -   5        $	 txtbxLozinka��5$      $
��Left=�6.   $��
10,130,0,0q�6I   $

 Wrap=�6^   $	��Top=�6r   $	��200��6�   $��20��6�   ���	#FF5D49EB6�   �� 5%      -   5        $	 txtbxidKorisnika��5%      $y�Hidden=�60   �� 5&      -   5        $	 txtbxidOsobe��5&      $y�Hidden=�6,   �� 5'      -	   5         ��Name$ btnUnesi��5'      $��Unesi��6)   $��590,10,30,170q�6;   $��20��6R   $��80��6^   ���	#FF5D49EB6�   �� 5(      -
   5        $ 	btnIzmeni��5(      $��Izmeni��6)   $��590,40,30,140q�6;   $��20��6R   $��80��6^   ���	#FF5D49EB6�   �� 5)      -   5        $ 	btnObrisi��5)      $��Obrisi��6)   $��590,70,30,110q�6;   $��20��6R   $��80��6^   ���	#FF5D49EB6�   �� 5*      -   5        $ 
btnPonisti��5*      $��Ponisti��6)   $��590,100,30,80q�6;   $��20��6R   $��80��6^   ���	#FF5D49EB6�   �� 5+      -   5        $ btnNazad��5+      $��Nazad��6)   $��10,10,590,170q�6;   $��20��6R   ���	#FF5D49EB6v   �� 5,      -   5        $ 	btnOsvezi��5,      $��Osvezi��6(   $��590,131,30,49q�69   $��20��6P   $��80��6\   ���	#FF5D49EB6   5-      5.      5/      