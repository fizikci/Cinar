Date.monthNames = ['Ocak','Şubat','Mart','Nisan','Mayıs','Haziran','Temmuz','Ağustos','Eylül','Ekim','Kasım','Aralık'];
if (Ext.util.Format) {
   Ext.util.Format.date = function(v, format) {
       if (!v) return "";
       if (!(v instanceof Date)) v = new Date(Date.parse(v));
       return v.dateFormat(format || "d/m/Y");
   };
}

if (Ext.DatePicker) {
   Ext.apply(Ext.DatePicker.prototype, {
       format: "d/m/Y"
   });
}

if (Ext.form.DateField) {
   Ext.apply(Ext.form.DateField.prototype, {
       format: "d/m/Y"
   });
}

if (Ext.grid.PropertyColumnModel) {
   Ext.apply(Ext.grid.PropertyColumnModel.prototype, {
       dateFormat: "j/m/Y"
   });
}
var pageSize = 20;

Ext.override(Ext.TabPanel, {
  changeTabIcon: function(item, icon){
    var el = this.getTabEl(item);
    if(el){
        Ext.fly(el).addClass('x-tab-with-icon').child('span.x-tab-strip-text').setStyle({backgroundImage:'url('+icon+')'});
    }
  }
});



function showEntityList(options)
{
    var tabPanel = Ext.getCmp('tabPanel');
	for(var i = 0; i < tabPanel.items.items.length; i++){
		var tab = tabPanel.items.items[i];
		if(tab.tag == (options.entityName + options.filter))
		{
			tabPanel.setActiveTab(i);
			return;
		}
	}

    var store = new Ext.data.JsonStore({
        // store configs
        url: 'Admin.ashx?method=getList&entityName=' + options.entityName + '&extraFilter=' + options.filter,
        storeId: options.entityName + 'Store',
        // reader configs
        root: 'root',
        totalProperty: 'totalCount',
        remoteSort: true,
        idProperty: 'Id',
        fields: options.fields,
		listeners: {
			exception: function(_this, type, action, options, response, arg){
				var res = eval('('+response.responseText+')');
				Ext.Msg.alert("Hata", res.errorMessage);
			},
			load: function() {
				//entityGrid.getSelectionModel().selectFirstRow();
				//entityGrid.getView().focusEl.focus();
				var currTab = Ext.getCmp('tabPanel').getActiveTab();
				if(!currTab.storeLoaded)
				{
					currTab.storeLoaded = true;
					newEntity();
				}
			}
		}
        //autoLoad: true
    });
    if(options.sortField)
		store.setDefaultSort(options.sortField, 'asc');
    
    var entityGrid = new Ext.grid.GridPanel({
        store: store,
        columns: options.columns,
        sm: new Ext.grid.RowSelectionModel({singleSelect:true}),
        anchor:'100% 100%',
        stripeRows: true,
        autoExpandColumn: 'name',
        height: 350,
        width: 600,
        bbar: new Ext.PagingToolbar({
            pageSize: pageSize,
            store: store,
            displayInfo: true,
            displayMsg: '{0}-{1} / {2}',
            emptyMsg: "Kayıt bulunamadı"
        })
    });
    
   	entityGrid.getSelectionModel().on('rowselect', function(sm, rowIdx, r) {
		loadForm(r.json.Id);
	});
	function loadForm(id){
		entityForm.getForm().load({
			url: 'Admin.ashx?method=getEntity&entityName='+options.entityName+'&id='+id,
			failure: function(form, action) {
				Ext.Msg.alert("Hata", action.result.errorMessage);
			}
		});
	}
    
    store.load({params:{start:0, limit:pageSize}});

    var entityForm = new Ext.FormPanel({
		trackResetOnLoad: true,
        labelAlign: 'top',
		id: options.formId,
        frame:true,
        title: 'İçerik Düzenle',
        bodyStyle:'padding:5px 5px 5px 5px',
        anchor: "-20",
        defaults: {
            itemCls: 'floatLeft'
        },
        items: options.editFormFields,
        buttons: [{
            text: 'KAYDET',
			icon: '../external/icons/_db_save.png',
			handler: formSubmit
        }]
    });
	
	function formSubmit(submitOpt){
		var basicForm = entityForm.getForm();
		var params = {};
		if(options.getParamsBeforeSave)
			options.getParamsBeforeSave(basicForm);
		basicForm.submit({
			url:'Admin.ashx?method=saveEntity&entityName='+options.entityName+'&filter='+options.filter, 
			params: params,
			waitMsg:'Kaydediliyor...', 
			submitEmptyText: false,
			success: function(form, action){
				//Ext.Msg.alert("Kaydedildi", action.result.errorMessage);
				store.load({params:{start:0, limit:pageSize}});
				if(submitOpt && submitOpt.afterSuccess)
					submitOpt.afterSuccess();
			},
			failure: function (form, action) { 
				Ext.MessageBox.alert('Hata', action.result.errorMessage); 
			} 
		});
	}

	function newEntity(){
		entityForm.getEl().child("input[type!=hidden]").focus();
		entityForm.getForm().load({
			url: 'Admin.ashx?method=newEntity&entityName='+options.entityName+'&filter='+options.filter,
			failure: function(form, action) {
				Ext.Msg.alert("Yükleme başarısız", action.result.errorMessage);
			}
		});
	}
	
	var centerItems = [];
	centerItems.push(entityForm);
	
    var tab = tabPanel.add({
        title: options.title,
		icon: options.icon,
        closable: true,
        layout:'border',
        tbar: [{
            text: 'Yeni',
			icon: '../external/icons/add.png',
            handler: function() {  
				newEntity();
			}
        }, /*{
            text: 'Düzenle',
			icon: '../external/icons/edit.png',
            handler: function() { showEntityList('Content'); }
        },*/ {
            text: 'Sil',
			icon: '../external/icons/delete.png',
            handler: function() {
				var e = entityGrid.getSelectionModel().getSelected();
				if(e.json.Id && confirm('Seçilen satır silinecek. Devam etmek istiyor musunuz?'))
					Ext.Ajax.request({
						url : 'Admin.ashx?method=deleteEntity&entityName='+options.entityName+'&id='+e.json.Id , 
						method: 'GET',
						success: function ( result, request ) { 
							//Ext.MessageBox.alert('İşlem Başarılı', result.responseText); 
							store.load({params:{start:0, limit:pageSize}});
							newEntity();
						},
						failure: function ( result, request) { 
							Ext.MessageBox.alert('İşlem Başarısız', result.responseText); 
						} 
					});
			}
		}],
        items: [{
            region:'west',
            split:true,
            width: 300,
            layout: 'anchor',
            border:false,
            items: entityGrid
        },{
            region:'center',
            layout: 'anchor',
            border:false,
			autoScroll: true,
            items : centerItems
        }]
    });
	tab.tag = options.entityName + options.filter;
	tab.storeLoaded = false;
	tabPanel.changeTabIcon(tab, options.icon);
    tabPanel.setActiveTab(tabPanel.items.length-1);
    tabPanel.doLayout();

}

function showContentList(catId, title)
{
	var formId = 'formContent' + catId;
	var formPanelItems = [];
	for(var i=0; i<langs.length; i++)
	{
		var fieldSuffix = langs[i].Code!=defLangCode ? '_lang_' + langs[i].Id : '';
		var tabTitle = langs.length==1 ? "İçerik" : langs[i].Name;
		formPanelItems.push({
			title: tabTitle,
			layout: 'form',
			frame: false,
			items: [
				{
					xtype: 'textfield',
					fieldLabel: 'Başlık',
					name: 'Title' + fieldSuffix,
					anchor: '48%'
				}, new Ext.form.TriggerField({
					xtype:'trigger',
					name: 'Picture' + fieldSuffix,
					fieldLabel: 'Resim',
					anchor: '48%',
					onTriggerClick: function(){
						showFileBrowser(Ext.getCmp(formId).findById(this.id));
					}
				}),{
					xtype:'htmleditor',
					name: 'Description' + fieldSuffix,
					fieldLabel: 'Açıklama',
					height: 80,
					anchor: '98%'
				},{
					xtype: 'htmleditor',
					name: 'Metin' + fieldSuffix,
					fieldLabel: 'Metin',
					height: 200,
					anchor: '98%'
				}
			]
		});
	}
	
	formPanelItems.push({
			title:'Detaylar',
			layout:'form',
			frame:false,
			items: [
				{
					xtype:'combo',
					fieldLabel: 'Türü',
					hiddenName: 'ClassName',
					anchor:'48%',
					store: new Ext.data.ArrayStore({
						fields: ['Id', 'Name'],
						data : [['Content','İçerik'],['Category','Kategori']]}
					),
					displayField: 'Name',
					valueField: 'Id',
					mode: 'local',
					forceSelection: true,
					typeAhead: true,
					triggerAction: 'all',
					autoSelect: true,
					selectOnFocus: true
				},{
					xtype:'combo',
					fieldLabel: 'Kategori',
					hiddenName: 'CategoryId',
					anchor:'48%',
					store: new Ext.data.ArrayStore({
						fields: ['Id', 'Name'],
						data : DATA.categories}
					),
					displayField: 'Name',
					valueField: 'Id',
					mode: 'local',
					forceSelection: true,
					typeAhead: true,
					triggerAction: 'all',
					autoSelect: true,
					selectOnFocus: true
				},{
					xtype:'combo',
					fieldLabel: 'Kaynak',
					hiddenName: 'SourceId',
					anchor:'48%',
					store: new Ext.data.ArrayStore({
						fields: ['Id', 'Name'],
						data : DATA.sources}
					),
					displayField: 'Name',
					valueField: 'Id',
					mode: 'local',
					forceSelection: true,
					typeAhead: true,
					triggerAction: 'all',
					autoSelect: true,
					selectOnFocus: true
				},{
					xtype:'combo',
					fieldLabel: 'Yazar',
					hiddenName: 'AuthorId',
					anchor:'48%',
					store: new Ext.data.ArrayStore({
						fields: ['Id', 'Name'],
						data : DATA.authors}
					),
					displayField: 'Name',
					valueField: 'Id',
					mode: 'local',
					forceSelection: true,
					typeAhead: true,
					triggerAction: 'all',
					autoSelect: true,
					selectOnFocus: true
				},{
					xtype:'datefield',
					fieldLabel: 'Yayın Tarihi',
					name: 'PublishDate',
					anchor:'48%',
					format: 'd/m/Y'
				},{
					xtype:'numberfield',
					fieldLabel: 'Sıra No',
					name: 'OrderNo',
					anchor:'23%',
						allowDecimals: false
				},{
				xtype:'combo',
					fieldLabel: 'Durum',
					hiddenName: 'Visible',
					anchor:'23%',
					store: new Ext.data.ArrayStore({
						fields: ['Id', 'Name'],
						data : [[1,'Aktif'],[0,'Pasif']]}
					),
					displayField: 'Name',
					valueField: 'Id',
					mode: 'local',
					forceSelection: true,
					typeAhead: true,
					triggerAction: 'all',
					autoSelect: true,
					selectOnFocus: true
				},{
					xtype:'textfield',
					name: 'Tags',
					fieldLabel:'Etiketler',
					anchor:'98%'
				}
			]
		}
	);

	
	showEntityList({
		title: title,
		entityName: 'Content', 
		icon: '../external/icons/category.png',
		formId: formId,
		filter: 'CategoryId='+catId,
		fields: [
            {name: 'Id'},
            {name: 'Content__Title'},
            {name: 'Content__PublishDate', type: 'date'}
        ],
		sortField: 'Content__Title',
		columns: [
            {id: 'name', header: 'Başlık', width: 160, sortable: true, dataIndex: 'Content__Title'},
            {header: 'Yayın Tarihi', width: 160, sortable: true, dataIndex: 'Content__PublishDate', renderer: Ext.util.Format.dateRenderer('d/m/Y')}
        ],
		editFormFields: [
			{
				xtype:'hidden',
				name: 'Id'
			},{
				xtype: 'tabpanel',
				//deferredRender: false,
				activeTab: 0,
				defaults: { autoHeight: true, bodyStyle: 'padding:10px' }, 
				frame: false,
				plain: true,
				items: formPanelItems
			}
		],
		getParamsBeforeSave: function(basicForm){
			var vals = basicForm.getValues();
			if(!vals.CategoryId)
				return {CategoryId: catId};
			else
				return null;
		}
	});
}

function showUserList()
{
	showEntityList({
		title: 'Kullanıcılar',
		entityName: 'User', 
		formId: 'formUser',
		icon: '../external/icons/user.png',
		filter: '',
		fields: [
            {name: 'Id'},
            {name: 'User__Email'},
            {name: 'User__Roles'}
        ],
		sortField: 'User__Email',
		columns: [
            {id: 'name', header: 'Kullanıcı', width: 160, sortable: true, dataIndex: 'User__Email'},
            {header: 'Rolleri', width: 160, sortable: true, dataIndex: 'User__Roles'}
        ],
		editFormFields: [
			{
				xtype:'hidden',
				name: 'Id'
			},{
				xtype:'textfield',
				name: 'Email',
                allowBlank:false,
				fieldLabel:'E-Posta',
				anchor:'48%'
			},{
                id:'selector1',
                xtype:'superboxselect',
                fieldLabel: 'Roller',
                resizable: true,
                name: 'Roles',
                anchor:'48%',
				store: new Ext.data.ArrayStore({
					fields: ['Id', 'Name'],
					data : [['User','User'],['Editor','Editor'],['Designer','Designer'],['Premium','Premium']]}
				),
                mode: 'local',
                displayField: 'Name',
                //displayFieldTpl: '{state} ({abbr})',
                valueField: 'Id',
				forceSelection : true
             },{
				xtype:'textfield',
				name: 'Password',
				fieldLabel:'Şifre',
				anchor:'23%',
				inputType: 'password'
			},{
				xtype:'textfield',
				name: 'Password2',
				fieldLabel:'Şifre (tekrar)',
				anchor:'23%',
				inputType: 'password'
			},{
				xtype:'textfield',
				name: 'Nick',
				fieldLabel:'Takma Ad',
				anchor:'23%'
			},{
				xtype:'combo',
				fieldLabel: 'Durum',
				hiddenName: 'Visible',
				anchor:'23%',
				store: new Ext.data.ArrayStore({
					fields: ['Id', 'Name'],
					data : [[1,'Aktif'],[0,'Pasif']]}
				),
				displayField: 'Name',
				valueField: 'Id',
				mode: 'local',
				forceSelection: true,
				typeAhead: true,
				triggerAction: 'all',
				autoSelect: true,
				selectOnFocus: true
			},{
				xtype:'textfield',
				name: 'Name',
				fieldLabel:'Ad',
				anchor:'48%'
			},{
				xtype:'textfield',
				name: 'Surname',
				fieldLabel:'Soyad',
				anchor:'48%'
			},{
				xtype:'textfield',
				name: 'Address1',
				fieldLabel:'Adres',
				anchor:'98%'
			},{
				xtype:'textfield',
				name: 'City',
				fieldLabel:'Şehir',
				anchor:'48%'
			},{
				xtype:'textfield',
				name: 'PhoneWork',
				fieldLabel:'Tel',
				anchor:'48%'
			}
		]
	});
}

function showAuthorList()
{
	showEntityList({
		title: 'İçerik Yazarları',
		entityName: 'Author', 
		formId: 'formAuthor',
		icon: '../external/icons/author.png',
		filter: '',
		fields: [
            {name: 'Id'},
            {name: 'NamedEntity__Name'}
        ],
		sortField: 'NamedEntity__Name',
		columns: [
            {id: 'name', header: 'Yazar', width: 160, sortable: true, dataIndex: 'NamedEntity__Name'}
        ],
		editFormFields: [
			{
				xtype:'hidden',
				name: 'Id'
			},{
				xtype:'textfield',
				name: 'Name',
                allowBlank:false,
				fieldLabel:'Yazar Adı',
				anchor:'73%'
			},{
				xtype:'combo',
				fieldLabel: 'Durum',
				hiddenName: 'Visible',
				anchor:'23%',
				store: new Ext.data.ArrayStore({
					fields: ['Id', 'Name'],
					data : [[1,'Aktif'],[0,'Pasif']]}
				),
				displayField: 'Name',
				valueField: 'Id',
				mode: 'local',
				forceSelection: true,
				typeAhead: true,
				triggerAction: 'all',
				autoSelect: true,
				selectOnFocus: true
			}, new Ext.form.TriggerField({
				xtype:'trigger',
				name: 'Picture',
				fieldLabel: 'Resim',
				anchor: '98%',
				onTriggerClick: function(){
					showFileBrowser(Ext.getCmp('formAuthor').findById(this.id));
				}
			}),{
				xtype:'htmleditor',
				name: 'Description',
				fieldLabel: 'Açıklama',
				height: 80,
				anchor: '98%'
			}
		]
	});
}

function showSourceList()
{
	showEntityList({
		title: 'İçerik Kaynakları',
		entityName: 'Source', 
		formId: 'formSource',
		icon: '../external/icons/source.png',
		filter: '',
		fields: [
            {name: 'Id'},
            {name: 'NamedEntity__Name'}
        ],
		sortField: 'NamedEntity__Name',
		columns: [
            {id: 'name', header: 'Kaynak', width: 160, sortable: true, dataIndex: 'NamedEntity__Name'}
        ],
		editFormFields: [
			{
				xtype:'hidden',
				name: 'Id'
			},
			{
				xtype:'textfield',
				name: 'Name',
                allowBlank:false,
				fieldLabel:'Kaynak Adı',
				anchor:'73%'
			},
			{
				xtype:'combo',
				fieldLabel: 'Durum',
				hiddenName: 'Visible',
				anchor:'23%',
				store: new Ext.data.ArrayStore({
					fields: ['Id', 'Name'],
					data : [[1,'Aktif'],[0,'Pasif']]}
				),
				displayField: 'Name',
				valueField: 'Id',
				mode: 'local',
				forceSelection: true,
				typeAhead: true,
				triggerAction: 'all',
				autoSelect: true,
				selectOnFocus: true
			}, 
			new Ext.form.TriggerField({
				xtype:'trigger',
				name: 'Picture',
				fieldLabel: 'Resim',
				anchor: '98%',
				onTriggerClick: function(){
					showFileBrowser(Ext.getCmp('formSource').findById(this.id));
				}
			}),
			{
				xtype:'htmleditor',
				name: 'Description',
				fieldLabel: 'Açıklama',
				height: 80,
				anchor: '98%'
			}
		]
	});
}

function showPollList()
{
	showEntityList({
		title: 'Anketler',
		entityName: 'PollQuestion', 
		icon: '../external/icons/Poll.png',
		filter: '',
		fields: [
            {name: 'Id'},
            {name: 'PollQuestion__Question'}
        ],
		sortField: 'PollQuestion__Question',
		columns: [
            {id: 'name', header: 'Anket', width: 160, sortable: true, dataIndex: 'PollQuestion__Question'}
        ],
		editFormFields: [
			{
				xtype:'hidden',
				name: 'Id'
			},{
				xtype:'textfield',
				name: 'Question',
                allowBlank:false,
				fieldLabel:'Anket Sorusu',
				anchor:'73%'
			},{
				xtype:'combo',
				fieldLabel: 'Durum',
				hiddenName: 'Visible',
				anchor:'23%',
				store: new Ext.data.ArrayStore({
					fields: ['Id', 'Name'],
					data : [[1,'Aktif'],[0,'Pasif']]}
				),
				displayField: 'Name',
				valueField: 'Id',
				mode: 'local',
				forceSelection: true,
				typeAhead: true,
				triggerAction: 'all',
				autoSelect: true,
				selectOnFocus: true
			},{
				xtype:'panel',
				fieldLabel: 'Cevaplar',
				anchor: '98%',
				html: 'Buraya cevaplar için editable grid gelecek.'
			}
		]
	});
}

function showRecommendationList()
{
	showEntityList({
		title: 'Ziyaretçi Tavsiyeleri',
		entityName: 'Recommendation', 
		icon: '../external/icons/Recommendation.png',
		filter: '',
		fields: [
            {name: 'BaseEntity__Id'},
            {name: 'Content'}
        ],
		columns: [
            {id: 'name', header: 'Tavsiye edilen sayfa', width: 160, sortable: true, dataIndex: 'Content'}
        ],
		editFormFields: [
			{
				xtype:'hidden',
				name: 'Id'
			},{
				xtype:'label',
				name: 'ContentId',
				fieldLabel:'Sayfa',
				anchor:'98%'
			},{
				xtype:'label',
				name: 'NameFrom',
				fieldLabel:'Tavsiye Eden Kişi',
				anchor:'48%'
			},{
				xtype:'label',
				name: 'EmailFrom',
				fieldLabel:'',
				anchor:'48%'
			},{
				xtype:'label',
				name: 'NameTo',
				fieldLabel:'Tavsiye Edilen Kişi',
				anchor:'48%'
			},{
				xtype:'label',
				name: 'EmailTo',
				fieldLabel:'',
				anchor:'48%'
			}
		]
	});
}

function showUserCommentList()
{
	showEntityList({
		title: 'Ziyaretçi Yorumları',
		entityName: 'UserComment', 
		icon: '../external/icons/UserComment.png',
		filter: '',
		fields: [
            {name: 'Id'},
            {name: 'Content'}
        ],
		columns: [
            {id: 'name', header: 'Tavsiye edilen sayfa', width: 160, sortable: true, dataIndex: 'Content'}
        ],
		editFormFields: [
			{
				xtype:'hidden',
				name: 'Id'
			},{
				xtype:'textfield',
				name: 'Email',
				fieldLabel:'Email Adresi',
				anchor:'73%'
			},{
				xtype:'combo',
				fieldLabel: 'Durum',
				hiddenName: 'Visible',
				anchor:'23%',
				store: new Ext.data.ArrayStore({
					fields: ['Id', 'Name'],
					data : [[1,'Aktif'],[0,'Pasif']]}
				),
				displayField: 'Name',
				valueField: 'Id',
				mode: 'local',
				forceSelection: true,
				typeAhead: true,
				triggerAction: 'all',
				autoSelect: true,
				selectOnFocus: true
			},{
				xtype:'textfield',
				name: 'IP',
				fieldLabel:'IP Adresi',
				anchor:'48%'
			},{
				xtype:'textfield',
				name: 'Nick',
				fieldLabel:'Takma Adı',
				anchor:'48%'
			},{
				xtype:'textfield',
				name: 'Web',
				fieldLabel:'Web Adresi',
				anchor:'48%'
			},{
				xtype:'textfield',
				name: 'Title',
				fieldLabel:'Başlık',
				anchor:'98%'
			},{
				xtype:'textarea',
				name: 'CommentText',
				fieldLabel:'Yorum',
				anchor:'98%'
			}
		]
	});
}

function showContactUsList()
{
	showEntityList({
		title: 'Ziyaretçi Mesajları',
		entityName: 'ContactUs', 
		icon: '../external/icons/ContactUs.png',
		filter: '',
		fields: [
            {name: 'Id'},
            {name: 'ContactUs__Email'},
            {name: 'ContactUs__Subject'}
        ],
		columns: [
            {id: 'name', header: 'Gönderen', width: 160, sortable: true, dataIndex: 'ContactUs__Email'},
            {id: 'name', header: 'Konu', width: 160, sortable: true, dataIndex: 'ContactUs__Subject'}
        ],
		editFormFields: [
			{
				xtype:'hidden',
				name: 'Id'
			},{
				xtype:'textfield',
				name: 'Name',
				fieldLabel:'Adı',
				anchor:'48%'
			},{
				xtype:'textfield',
				name: 'Email',
				fieldLabel:'Eposta adresi',
				anchor:'48%'
			},{
				xtype:'textfield',
				name: 'Subject',
				fieldLabel:'Başlık',
				anchor:'98%'
			},{
				xtype:'textarea',
				name: 'Message',
				fieldLabel:'Mesaj',
				anchor:'98%'
			}
		]
	});
}

function showStyles()
{
	var options = {
		title: 'Sitil Dosyası (CSS)',
		icon: '../external/icons/css.png'
	};
	
    var tabPanel = Ext.getCmp('tabPanel');
	for(var i = 0; i < tabPanel.items.items.length; i++){
		var tab = tabPanel.items.items[i];
		if(tab.tag == "css")
		{
			tabPanel.setActiveTab(i);
			return;
		}
	}


    var entityForm = new Ext.FormPanel({
        labelAlign: 'top',
        frame: true,
		title: 'Burada yapacağınız değişiklikler sitenizin tüm sayfalarını etkileyecektir',
        bodyStyle:'padding:5px 5px 5px 5px',
        layout: "fit",
        items: {
			xtype: 'textarea',
			name: 'text'
		},
        buttons: [{
            text: 'KAYDET',
			icon: '../external/icons/_db_save.png',
			handler: formSubmit
        }],
		listeners: {
			render: function(_this){
				_this.getForm().load({
					url: 'Admin.ashx?method=getCSS',
					failure: function(form, action) {
						Ext.Msg.alert("Hata", action.result.errorMessage);
					}
				});
			}
		}
    });
	
	function formSubmit(){
		var basicForm = entityForm.getForm();
		var params = {};
		if(options.getParamsBeforeSave)
			options.getParamsBeforeSave(basicForm);
		basicForm.submit({
			url:'Admin.ashx?method=saveCSS', 
			params: params,
			waitMsg:'Kaydediliyor...', 
			submitEmptyText: false,
			success: function(form, action){
				Ext.Msg.alert("Kaydedildi", action.result.errorMessage);
			},
			failure: function (form, action) { 
				Ext.Msg.alert('Hata', action.result.errorMessage); 
			} 
		});
	}

    var tab = tabPanel.add({
        title: options.title,
		icon: options.icon,
        closable: true,
        layout:'fit',
        items: entityForm
    });
	tab.tag = "css";
	tabPanel.changeTabIcon(tab, options.icon);
    tabPanel.setActiveTab(tabPanel.items.length-1);
    tabPanel.doLayout();
}

function showConfiguration()
{
	var options = {
		title: 'Genel Ayarlar',
		icon: '../external/icons/Configuration.png'
	};
	
    var tabPanel = Ext.getCmp('tabPanel');
	for(var i = 0; i < tabPanel.items.items.length; i++){
		var tab = tabPanel.items.items[i];
		if(tab.tag == "Configuration")
		{
			tabPanel.setActiveTab(i);
			return;
		}
	}


    var entityForm = new Ext.FormPanel({
        labelAlign: 'top',
        frame: true,
		//title: 'Burada yapacağınız değişiklikler sitenizin tüm sayfalarını etkileyecektir',
        bodyStyle:'padding:5px 5px 5px 5px',
        //layout: "fit",
        items: {
			xtype: 'textarea',
			name: 'text'
		},
        buttons: [{
            text: 'KAYDET',
			icon: '../external/icons/_db_save.png',
			handler: formSubmit
        }],
		listeners: {
			render: function(_this){
				_this.getForm().load({
					url: 'Admin.ashx?method=getConfiguration',
					failure: function(form, action) {
						Ext.Msg.alert("Hata", action.result.errorMessage);
					}
				});
			}
		}
    });
	
	function formSubmit(){
		var basicForm = entityForm.getForm();
		var params = {};
		if(options.getParamsBeforeSave)
			options.getParamsBeforeSave(basicForm);
		basicForm.submit({
			url:'Admin.ashx?method=saveConfiguration', 
			params: params,
			waitMsg:'Kaydediliyor...', 
			submitEmptyText: false,
			success: function(form, action){
				Ext.Msg.alert("Kaydedildi", action.result.errorMessage);
			},
			failure: function (form, action) { 
				Ext.Msg.alert('Hata', action.result.errorMessage); 
			} 
		});
	}

    var tab = tabPanel.add({
        title: options.title,
		icon: options.icon,
        closable: true,
        layout:'fit',
        items: entityForm
    });
	tab.tag = "Configuration";
	tabPanel.changeTabIcon(tab, options.icon);
    tabPanel.setActiveTab(tabPanel.items.length-1);
    tabPanel.doLayout();
}
