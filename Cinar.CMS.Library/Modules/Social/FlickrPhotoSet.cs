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
    jQuery.ajax({
        url: 'http://api.flickr.com/services/rest/?method=flickr.photosets.getPhotos&api_key=156ed26be4fb59ae308b2a79e8d78d0d&photoset_id=$=entity.Description$&per_page=500&format=json&nojsoncallback=1',
        dataType: 'json',
        success: function (data) {
            var row = """";
            for (var i = 0; i < data.photoset.photo.length; i++) {
                var p = data.photoset.photo[i];
                row += ""<a href='http://farm"" + p.farm+"".staticflickr.com/""+p.server+""/""+p.id+""_""+p.secret+""_b.jpg"" + ""' data-lightbox='roadtrip$=this.Id$'><img src='http://farm"" + p.farm+"".staticflickr.com/""+p.server+""/""+p.id+""_""+p.secret+""_n.jpg"" + ""' height='30' /></a>"";
            }
            jQuery("".fotoGaleriThumbs"").html(row);
        },
        error: function () {
            alert(""Error loading flickr image results"");
        }
    });
});
function openFotoGaleri(id){
    if(!id){
        selectFlickrPhotoSet('$=this.FlickrUserId$', function(elm){
            elm = jQuery(elm).parent(); 
            var entity = {
                Description: elm.find('.pid').text(),
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
            showRelatedEntities: ['ContentLang','ContentPicture'],
            filter: 'CategoryId=$=this.ParentCategoryId$ AND ClassName=Content AND Picture2= AND Keywords= AND IsManset=0 AND SpotTitle= AND SourceId=1 AND SourceLink=',
            callback: function(){location.reload();}
        });    
}
</script>
<button class=""editorButton"" onclick=""openFotoGaleri(0)""><span class=""fff add""></span> Flickr Foto Galeri Ekle</button>
";
        }
    }
}
