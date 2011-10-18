function showFileBrowser(forControl){

	var uploadFolder = '';
	var selectedFile = '';

	var storeFiles = new Ext.data.JsonStore({
					url: 'Admin.ashx?method=getFileList',
					root: 'root',
					totalProperty: 'totalCount',
					remoteSort: true,
					idProperty: 'FileName',
					fields: [
						{name: 'FileName'},
						{name: 'Size', type: 'int'},
						{name: 'DateModified', type: 'date', dateFormat:'j-n-Y G:i'}
					],
					listeners: {
						exception: function(_this, type, action, options, response, arg){
							var res = eval('('+response.responseText+')');
							Ext.Msg.alert("Hata", res.errorMessage);
						},
						load: function() {
						}
					}
				});
				
	var winFileBrowser = new Ext.Window( new Ext.Viewport({
		title: 'Dosya Yükle / Seç',
		width: 640,
		height: 480,
		layout: 'border',
		tbar: [
			{
				xtype:'button',
				text:'Yükle',
				icon:'../external/icons/add.png',
				handler: function(button){
					var uploadDialog = new Ext.ux.UploadDialog.Dialog({
						url: 'Admin.ashx?method=uploadFile&folder='+uploadFolder,
						reset_on_hide: false,
						allow_close_on_upload: true,
						upload_autostart: false, //true,
						post_var_name: 'upload',
						base_params: { action : 'uploadfile', resptype: 'json' }
					});

					uploadDialog.on('uploadcomplete', function(){
						storeFiles.proxy.setUrl('Admin.ashx?method=getFileList&folder=' + uploadFolder);
						storeFiles.load();
					});
					//uploadDialog.on('beforefileuploadstart', onBeforeFileUploadStart);
					//uploadDialog.on('uploadcanceled', function() { console.log('cancel');});

					uploadDialog.show();
				}
			},
			{
				xtype:'button',
				text:'Sil',
				icon:'../external/icons/delete.png',
				handler: function(){
					Ext.Msg.confirm('Çınar CMS', selectedFile + ' silinecek. Devam etmek istiyor musunuz?', function(btn, text){
						if (btn == 'yes'){
							Ext.Ajax.request({
								url : 'Admin.ashx?method=deleteFile&fileName=' + selectedFile + '&folder=' + uploadFolder, 
								method: 'GET',
								success: function ( result, request ) { 
									storeFiles.proxy.setUrl('Admin.ashx?method=getFileList&folder=' + uploadFolder);
									storeFiles.load();
									selectedFile = '';
								},
								failure: function ( result, request) { 
									Ext.MessageBox.alert('İşlem Başarısız', result.responseText); 
								} 
							});
						}
					});
				}
			},
			'-',
			{
				xtype:'button',
				text:'Seçtiğim dosyayı kullan',
				icon:'../external/icons/add.png',
				handler: function(button){
					forControl.setValue('UserFiles'+uploadFolder+'/'+selectedFile);
					winFileBrowser.close();
				}
			}
		],
		items: [
			{
				region: 'east',
				title: 'İşlemler',
				collapsible: true,
				collapsed: true,
				split: true,
				width: 225, 
				minSize: 175,
				maxSize: 400,
				margins: '0 5 0 0',
				layout: 'fit',
				items: {
					html:'Bu pencereyi sitenize dosya yüklemek ve daha önce yüklediğiniz dosyalardan birini seçmek için kullanabilirsiniz. <ul><li>Sol taraftaki ağaç görünümünü kullanarak klasörleri dolaşabilirsiniz.</li><li>"Yükle" butonuna tıklayarak sitenize yeni dosya(lar) yükleyin</li><li>Listeden bir dosya seçip "Sil" butonuna tıklayarak seçtiğiniz dosyayı silin.</li><li>"Seç" butonuna tıklayarak dosyanızın seçilmesini sağlayın.</li></ul>'
				}
			}, 
			{
				region: 'west',
				title: 'Klasörler',
				split: true,
				width: 200,
				minSize: 175,
				maxSize: 400,
				collapsible: true,
				margins: '0 0 0 5',
				layout: 'fit',
				items: {
					xtype: 'treepanel',
					id: 'treeFiles',
					anchor: '100% 100%',
					margins: '2 2 0 2',
					autoScroll: true,
					rootVisible: false,
					root: new Ext.tree.AsyncTreeNode(),
					loader: new Ext.tree.TreeLoader({
						url: 'Admin.ashx?method=getFolderList',
						createNode: function(attr) {
							if (attr.isConsolidation) {
								attr.iconCls = 'x-consol',
									attr.allowDrop = true;
							}
							return Ext.tree.TreeLoader.prototype.createNode.call(this, attr);
						}
					}),
					listeners: {
						'render': function(tp) {
							tp.on('beforeclick', function(node, e) {
								uploadFolder = node.id;
								storeFiles.proxy.setUrl('Admin.ashx?method=getFileList&folder=' + node.id);
								storeFiles.load();
							});
						}
					}
				}
			},
			{
				region:'center',
				split:true,
				width: 300,
				layout: 'anchor',
				border:false,
				items: new Ext.grid.GridPanel({
					region: 'center',
					store: storeFiles,
					columns: [
						{id: 'name', header: 'Dosya Adı', width: 160, sortable: true, dataIndex: 'FileName'},
						{header: 'Boyut', width: 60, sortable: true, dataIndex: 'Size'},
						{header: 'Değ. Tarihi', width: 60, sortable: true, dataIndex: 'DateModified', renderer: Ext.util.Format.dateRenderer('d.m.Y')}
					],
					sm: new Ext.grid.RowSelectionModel({
						singleSelect:true,
						listeners: {
							rowselect: function(sm, rowIdx, r) {
								selectedFile = r.json.FileName;
							}
						}
					}),
					anchor:'100% 100%',
					stripeRows: true,
					autoExpandColumn: 'name',
					height: 350,
					width: 600
				})
			}
		]
	})
	).show();
	
	storeFiles.load();
}
