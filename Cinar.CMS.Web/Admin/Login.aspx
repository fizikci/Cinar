<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Çınar CMS Yönetim Paneli</title>
    <link href="../external/resources/css/ext-all.css" rel="stylesheet" type="text/css" />
    <script src="../external/adapter/ext/ext-base.js" type="text/javascript"></script>
    <script src="../external/ext-all.js" type="text/javascript"></script>
    <script>
Ext.onReady(function(){

    Ext.QuickTips.init();

    // turn on validation errors beside the field globally
    Ext.form.Field.prototype.msgTarget = 'side';
    
    var login_form = new Ext.FormPanel({
        id: login_form,
        labelWidth: 75, 
        frame:true,
        title: 'Oturum Aç',
        bodyStyle:'padding:5px 5px 0',
        width: 350,
        defaults: {width: 230},
        onSubmit: Ext.emptyFn,
        submit: function() {
                this.getForm().getEl().dom.action = 'DoLogin.ashx';
                this.getForm().getEl().dom.submit();
                },
        defaultType: 'textfield',
        items: [{
                fieldLabel: 'Email',
                name: 'Email',
                //vtype:'email',
                allowBlank:false
            },{
                fieldLabel: 'Password',
                inputType:'password',
                name: 'Passwd',
                allowBlank:false 
            },{
                name: 'RedirectURL',
                inputType:'hidden',
                value: 'Default.aspx'
            }
        ],
        buttons: [{
            text: 'Tamam',
            handler: submitForm
        }]
    });
    
   
    function submitForm(){
        login_form.submit();
    }
    
    login_form.render('login_form');
    login_form.getEl().child("input[type!=hidden]").focus();

});  
    </script>
</head>
<body style="text-align:center">
<div id="login_form" style="margin-left:auto;margin-right:auto;margin-top:200px;width:400px;text-align:left"></div>
</body>
</html>
