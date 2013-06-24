<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" %>

<script runat="server">
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="page-content">
    <div id="breadcrumbs">
        <ul class="breadcrumb">
            <li>
                <i class="icon-home"></i>
                <a href="/Admin/Default.aspx">Ana Sayfa</a>

                <span class="divider">
                    <i class="icon-angle-right"></i>
                </span>
            </li>
            <li>Kullanıcı Yönetimi
							<span class="divider">
                                <i class="icon-angle-right"></i>
                            </span>

            </li>
            <li class="active">Kullanıcılar</li>
        </ul>
        <!--.breadcrumb-->

        <div id="nav-search">
            <form class="form-search">
                <span class="input-icon">
                    <input type="text" placeholder="Search ..." class="input-small search-query" id="nav-search-input" autocomplete="off" />
                    <i class="icon-search" id="nav-search-icon"></i>
                </span>
            </form>
        </div>
        <!--#nav-search-->
    </div>

    <div class="row-fluid">
        <h3 class="header smaller lighter blue">Kullanıcılar</h3>
        <div class="table-header">
            <button onclick="CRUD.newEntity()" class="btn btn-mini btn-success"><i class="icon-ok bigger-120"></i> +Yeni Kullanıcı Ekle</button>
        </div>
        <table id="table_report" class="table table-striped table-bordered table-hover">
            <thead>
                <tr>
                    <th class="center">
                        <label>
                            <input type="checkbox" />
                            <span class="lbl"></span>
                        </label>
                    </th>
                    <th>Email</th>
                    <th>Adı</th>
                    <th class="hidden-480">Soyadı</th>
                    <th></th>
                </tr>
            </thead>

            <tbody>
            </tbody>
        </table>

        <div id="userForm" class="widget-box" style="display:none">
            <div class="page-header position-relative">
                <h1>Kullanıcı Düzenle
					<small>
                        <i class="icon-double-angle-right"></i>
                        Kullanıcı bilgilerini giriniz
                    </small>
                </h1>
            </div>
            <div class="row-fluid">
                <input type="hidden" name="Id" id="Id"/>
                <div class="span8">
                    <div class="control-group">
                        <label class="control-label" for="form-field-1">Ad</label>
                        <div class="controls">
                            <input type="text" name="Name" id="Name" placeholder="Username"/>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="form-field-1">Soyad</label>
                        <div class="controls">
                            <input type="text" name="Surname" id="Surname" placeholder="Username"/>
                        </div>
                    </div>
                </div>
                <div class="span4">
                </div>
            </div>
        </div>
    </div>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
     <script src="/Admin/external/default.js"></script>
    <script>
        $(function () {
            CRUD.entityName = 'User';
            CRUD.getTableRowHtml = function (entity) {
                return '<tr>' +
                            '<td class="center"><label><input type="checkbox" /><span class="lbl"></span></label></td>' +
                            '<td>' + entity.Email + '</td>' +
                            '<td>' + entity.Name + ' (' + entity.Id + ')' + '</td>' +
                            '<td class="hidden-480">' + entity.Surname + '</td>' +
                            '<td class="td-actions">' +
                                '<button onclick="CRUD.editEntity(' + entity.Id + ')" class="btn btn-mini btn-info"><i class="icon-edit bigger-120"></i></button>' +
                                '<button onclick="CRUD.deleteEntity(' + entity.Id + ')" class="btn btn-mini btn-danger"><i class="icon-trash bigger-120"></i></button>' +
                            '</td>' +
                        '</tr>';
            };

            CRUD.entityListBind();
        });
    </script>
</asp:Content>

