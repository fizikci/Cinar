var leftMenu = [
	{
        title: 'Site İçeriği ve Kategoriler',
        border: false,
        layout: 'anchor',
        autoScroll: true,
		items: [
			{
				xtype: 'treepanel',
				id: 'treeCats',
				anchor: '100% 100%',
				margins: '2 2 0 2',
				autoScroll: true,
				rootVisible: false,
				root: new Ext.tree.AsyncTreeNode(),
				loader: new Ext.tree.TreeLoader({
					url: 'Admin.ashx?method=getCategories',
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
						/*tp.getSelectionModel().on('selectionchange', function(tree, node) {
							showContentList(node.id, node.text);
						});*/
						tp.on('beforeclick', function(node, e) {
							showContentList(node.id, node.text);
						});
					}
				}
			}
		]
	},
	{
	    title: 'Detaylar',
	    border: false,
	    layout: {
			type:'vbox',
			padding:'5',
			align:'stretch'
		},
	    defaults: {
			margins:'0 0 5 0', 
			xtype:'button',
			iconAlign: 'top'
		},
	    items: [
			{
				text: 'Site Üyeleri',
				icon: '../external/icons/user.png',
				handler: function() {
					showUserList();
				}
			}, 
			{
				text: 'İçerik Yazarları',
				icon: '../external/icons/author.png',
				handler: function() {
					showAuthorList();
				}
			}, 
			{
				text: 'İçerik Kaynakları',
				icon: '../external/icons/source.png',
				handler: function() {
					showSourceList();
				}
			}, 
			/*{
				text: 'Anketler',
				icon: '../external/icons/poll.png',
				handler: function() {
					showPollList();
				}
			},*/ 
			{
				text: 'Ziyaretçi Tavsiyeleri',
				icon: '../external/icons/recommendation.png',
				handler: function() {
					showRecommendationList();
				}
			}, 
			{
				text: 'Ziyaretçi Yorumları',
				icon: '../external/icons/UserComment.png',
				handler: function() {
					showUserCommentList();
				}
			}, 
			{
				text: 'Ziyaretçi Mesajları',
				icon: '../external/icons/ContactUs.png',
				handler: function() {
					showContactUsList();
				}
			}
		]
	},
	{
		title: 'Ayarlar',
		border: false,
	    layout: {
			type:'vbox',
			padding:'5',
			align:'stretch'
		},
	    defaults: {
			margins:'0 0 5 0', 
			xtype:'button',
			iconAlign: 'top'
		},
	    items: [
			{
				text: 'Genel Ayarlar',
				icon: '../external/icons/configuration.png',
				handler: function() {
					showEntityList("Author");
				}
			}, {
				text: 'Stil Dosyası (CSS)',
				icon: '../external/icons/css.png',
				handler: function() {
					showStyles();
				}
			}
		]
	}
];
