   M S B A M L   `   `   `  ����B  >Bioskop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Bioskop.StraniceBioskop.Stranice  5      &     Bioskop.Stranice.stranicaRadnici   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Bioskop.Stranice     5        N� Title$  stranicaRadnici��5	       N� 
Background �	#FFD1D2FF.��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��DataGrid��5         AutoGenerateColumns .  $��10, 215, 10, 10q�6?      
IsReadOnly .    AlternatingRowBackground
 ����� �� AlternationCount$ 2��6�   ���	#FF5D49EB   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$	 Ime��6=      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6J   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6%   �� *ime+6J     5      $ Prezime��6A   $ * 6R   	 6%   �� *	prezime+6R     5      $ Pozicija��6B   $ * 6T   	 6%   �� *
pozicija+6T     5      $ Telefon��6A   $ * 6R   	 6%   �� *	telefon+6R     5      $
 JMBG��6>   $ * 6L   	 6%   �� *jmbg+6L     5      $ Adresa��6@   $ * 6P   	 6%   �� *adresa+6P     5      $ Plata��6?   $ * 6N   	 6%   �� *plata+6N     5      $ ID��6C   $ * 6O   	 6%   �� *	idRadnika+6O   5      � 5   
   $��Center=�6   $	��210��6,   $	��700��69   $	��Top=�6E   .+�5      �� $	��Ime��6   $
��Left=�60   $��215,156,0,0q�6K   $��14(�6b   $	��Top=�6v   �� 5      $��Prezime��6   $
��Left=�62   $��215,181,0,0q�6M   $��14(�6c   $	��Top=�6w   �� 5      $��Adresa��6   $
��Left=�6*   $��
215,66,0,0q�6E   $��14(�6Z   $	��Top=�6n   �� 5      $��Pozicija��6   $
��Left=�66   $��
498,35,0,0q�6Q   $��14(�6e   $	��Top=�6y   �� 5      $��Telefon��6   $
��Left=�65   $��
215,96,0,0q�6P   $��14(�6e   $	��Top=�6y   �� 5      $
��JMBG��6   $
��Left=�62   $��215,126,0,0q�6M   $��14(�6b   $	��Top=�6v   �� 5       $��Plata��6   $
��Left=�63   $��
215,36,0,0q�6N   $��14(�6b   $	��Top=�6v   �� 5!      $��RADNICI��6   $
��Left=�61   $��
115,10,0,0q�6L   $	��Top=�6`   $��30��6x   $��95��6�   $��16(�6�   ���	#FF5D49EB$
��Bold"�6�   �� 5"      -   5        
 ��Name$
 txtbxIme��5"      $
��Left=�62   $��
10,160,0,0q�6M    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $	��200��6�   $��20��6�   ���	#FF5D49EB6�   �� 5#      -   5        $
 txtbxPrezime��5#      $
��Left=�64   $��
10,185,0,0q�6O   $
 Wrap=�6e   $	��Top=�6y   $	��200��6�   $��20��6�   ���	#FF5D49EB6�   �� 5$      -   5        $
 txtbxAdresa��5$      $
��Left=�66   $��	10,70,0,0q�6Q   $
 Wrap=�6f   $	��Top=�6z   $	��200��6�   $��20��6�   ���	#FF5D49EB6�   �� 5%      -   5        $
 txtbxTelefon��5%      $
��Left=�6.   $��
10,100,0,0q�6I   $
 Wrap=�6^   $	��Top=�6r   $	��200��6�   $��20��6�   ���	#FF5D49EB6�   �� 5&      -   5        $
 	txtbxJmbg��5&      $
��Left=�6+   $��
10,130,0,0q�6F   $
 Wrap=�6[   $	��Top=�6o   $	��200��6�   $��20��6�   ���	#FF5D49EB6�   �� 5'      -   5        $
 
txtbxPlata��5'      $
��Left=�6,   $��	10,40,0,0q�6G   $
 Wrap=�6[   $	��Top=�6o   $	��200��6�   $��20��6�   ���	#FF5D49EB6�   �� 5(      -   5        $
 txtbxidRadnika��5(      $y�Hidden=�6.   �� 5)      -	   5        $
 txtbxidOsobe��5)      $y�Hidden=�6,   �� 5*      -
   5        $��cmbbxPozicija��5*      $��293,36,207,144q�6,   ���	#FF5D49EB.@�5+      �� .��6   
Menadzer6)   �� 5,      .��6   
Cistac/ica6+   �� 5-      .��6   Prodavac karata60   �� 5.      .��6   Prodavac hrane6/   �� 5/      .��6   Administrator6.   �� 50      .��6   Tehnicko osoblje61   51      �� 52      -   5         ��Name$ btnUnesi��52      $��Unesi��6)   $��590,10,30,170q�6;   $��20��6R   $��80��6^   ���	#FF5D49EB6�   �� 53      -   5        $ 	btnIzmeni��53      $��Izmeni��6)   $��590,40,30,140q�6;   $��20��6R   $��80��6^   ���	#FF5D49EB6�   �� 54      -   5        $ 	btnObrisi��54      $��Obrisi��6)   $��590,70,30,110q�6;   $��20��6R   $��80��6^   ���	#FF5D49EB6�   �� 55      -   5        $ 
btnPonisti��55      $��Ponisti��6)   $��590,100,30,80q�6;   $��20��6R   $��80��6^   6i   �� 56      -   5        $ btnNazad��56      $��Nazad��6)   $��10,15,590,175q�6;   $��20��6R   ���	#FF5D49EB6u   57      58      59      