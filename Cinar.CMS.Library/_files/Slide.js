/*
Bu kod ContentListByFilter'ýn clItem'larýný slide eder
*/

document.observe("dom:loaded", function() {
  var container = $('ContentListByFilter_392');
  var stackItems = new Element('div', {style:'display:none'});
  while(container.down('div.clItem'))
    stackItems.insert(container.down('div.clItem').remove());
  container.insert(stackItems.down().remove());

  new PeriodicalExecuter(function(pe) {
    stackItems.insert(container.down('div.clItem').remove());
    container.insert(stackItems.down().remove());
    container.down('div.clItem').hide();
    Effect.Appear(container.down('div.clItem'));
  }, 8);
});