<script>
Event.observe(window, 'load', startDescriptionHide);

function hideAllDesc(){
   $$('#ContentListByFilter_129 div').each(
      function(elm){
         if(elm.className=='clDesc')
             elm.hide();
      }
   );
}

function startDescriptionHide(){
   hideAllDesc();
   $$('#ContentListByFilter_129 a').each(
      function(elm){
         if(elm.up().className=='clTitle'){
            elm.observe('click', showHideDesc);
         }
      }
   );
}
var __link_129;
function showHideDesc(e){
   hideAllDesc();
   var link = $(Event.element(e));
   var elm = link.up('div.clItem').down('div.clDesc');
   if(link==__link_129)
      {elm.hide(); __link_129 = null;}
   else
      {elm.show(); __link_129 = link;}
   Event.stop(e);
}
</script>