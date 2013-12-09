using Cinar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinar.CMS.Library.Modules
{
    [ModuleInfo(Grup = "Social")]
    public class FlickrPhotoSet : StaticHtml
    {

        protected int parentCategoryId = 1;
        [ColumnDetail(IsNotNull = true, DefaultValue = "1", References = typeof(Entities.Content))]
        [EditFormFieldProps(ControlType = ControlType.LookUp, Options = "extraFilter:'ClassName=Category'")]
        public int ParentCategoryId
        {
            get { return parentCategoryId; }
            set { parentCategoryId = value; }
        }

        public string FlickrApiKey { get; set; }
        public string FlickrUserId { get; set; }

        public FlickrPhotoSet()
        {
            this.InnerHtml = @"
<div class=""fotoGaleriThumbs"">
</div>
<script src=""/external/javascripts/lightbox/js/lightbox-2.6.min.js""></script>
<link href=""/external/javascripts/lightbox/css/lightbox.css"" rel=""stylesheet"" />
<script>
jQuery(function(){
    getFlickrPhotos('$=this.FlickrApiKey$', '$=entity.ExtraField1$', function (photos) {
        var row = """";
        for (var i = 0; i < photos.length; i++) {
            row += ""<a href='""+photos[i].photoUrl+""' data-lightbox='roadtrip$=this.Id$'><img src='""+photos[i].thumbUrl+""' height='30' /></a>"";
        }
        jQuery("".fotoGaleriThumbs"").html(row);
    });
});    

function openFotoGaleri(id){
    if(!id){
        selectFlickrPhotoSet('$=this.FlickrApiKey$', '$=this.FlickrUserId$', function(elm){
            elm = jQuery(elm).parent(); 
            var entity = {
                ExtraField1: elm.find('.pid').text(),
                Title: elm.find('.ptit').text(),
                CategoryId:$=this.ParentCategoryId$,
                ClassName:'Content',
                Picture: elm.find('img').attr('src').replace('_n.','_b.')
            };
            saveEntity('Content', entity, function(entity){
                Windows.getFocusedWindow().close();
                location.reload();
            });
        });
    }
    else
        openEntityEditForm({
            entityName: 'Content', 
            id: id,
            title: 'Foto Galeri Düzenle',
            hideCategory: 'İçerik,Temel,Extra',
            hideFilter:true,
            showRelatedEntities: ['ContentLang'],
            filter: 'CategoryId=$=this.ParentCategoryId$ AND ClassName=Content AND Picture2= AND Keywords= AND IsManset=0 AND SpotTitle= AND SourceId=1 AND SourceLink=',
            callback: function(){location.reload();}
        });    
}
</script>
<button class=""editorButton"" onclick=""openFotoGaleri(0)""><span class=""fff picture_add""></span> Flickr Foto Galeri Ekle</button>
";
        }
    }
}
