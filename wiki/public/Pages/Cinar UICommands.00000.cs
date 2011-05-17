Çınar UI Commands
admin|2011/05/17 01:14:42
##PAGE##
==Tanım==
Adobe Photoshop, Microsoft Visual Studio gibi programların UI programlamada kullandıkları pattern; Command Pattern'dir. Çınar UI Commands kütüphanesi ile bu tasarım kalıbını uygulamalarınıza kazandırabilirsiniz.

==Avantajları==
* "Test edilebilir kod" yazmanızı sağlar.
* Birden çok eventin aynı komutu tetikleyebilmesini sağlar.
* Komutun çalıştırabilir durumda olup/olmadığını anlayarak bu komutu tetikleyen UI kontrollerinin otomatik olarak enable/disable veya visible/invisible olmasını sağlar.
* Çalıştırılan komutları history'sinde saklayarak Undo/Redo işlevselliğini mümkün kılar.
* Tek merkezden bütün komutların askıya alınmasını ve sonra tekrar aktif hale getirilmesini mümkün kılar.
* Kodunuzun daha dekleratif, daha anlaşılır ve daha ölçeklenebilir olmasını sağlar.

==Örnek Kod==
@@
CommandManager cmdMan = new CommandManager();

cmdMan.Commands = new List<Command>{
	new Command {
		Execute = cmdListFatura,
		Trigger = new CommandTrigger { Control = menuListFatura },
		IsVisible = () => Context.CurrentUser.HasRole("Muhasebe")
	},
	new Command {
		Execute = cmdDeleteFatura,
		Trigger = new CommandTrigger { Control = btnDeleteFatura },
		IsEnabled = () => SelectedFatura != null
	}
};

cmdMan.SetCommandTriggers();
@@

Yukarıdaki örnekte;
* menuListFatura isimli menü öğesine tıklandığında çalışacak olan cmdListFatura komutu tanımlanmış.
* Eğer mevcut kullanıcı Muhasebe grubunda değilse bu menü öğesinin görünmemesi sağlanıyor.
* Aynı şekilde btnDeleteFatura butonuna tıklandığında çalışacak olan cmdDeleteFatura komutu tanımlanmış.
* Eğer seçili faturu yoksa butonun disable edilmesi gerektiği belirtilmiş.
* Son satırda SetCommandTriggers() metodu ile kontrollerle komutlar arası event bağlamının oluşturulması sağlanmış.