using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraLayout.Localization;
using DevExpress.XtraNavBar;

namespace Cinar.WinUI
{
    public class GridLocalizerTr : GridLocalizer
    {
        public override string GetLocalizedString(GridStringId id)
        {
            switch (id)
            {
                case GridStringId.FilterBuilderCancelButton: return "&İptal";
                case GridStringId.LayoutViewLabelCardEdgeAlignment: return "Kart Kenarı Sıralaması:";
                case GridStringId.LayoutViewGroupPropertyGrid: return "Özellik Tablosu";
                case GridStringId.LayoutViewLabelShowCardCaption: return "Başlık Göster";
                case GridStringId.PopupFilterNonBlanks: return "(Boş olmayan alanlar)";
                case GridStringId.CustomFilterDialogCancelButton: return "&İptal";
                case GridStringId.LayoutViewLabelTextAlignment: return "Alan Başlığı Metin Hizalaması:";
                case GridStringId.MenuColumnClearSorting: return "Sıralamayı Temizle";
                case GridStringId.FilterBuilderCaption: return "Filtre Oluşturucu";
                case GridStringId.GridNewRowText: return "Yeni bir satır eklemek için buraya tıkla";
                case GridStringId.MenuColumnSortAscending: return "Artarak Sıralat";
                case GridStringId.LayoutViewLabelShowCardExpandButton: return "Genişleme Düğmesini Göster";
                case GridStringId.CustomFilterDialogRadioOr: return "&Ya da";
                case GridStringId.CustomizationFormBandHint: return "Düzeni özelleştirmek için bantları buraya taşı bırak";
                case GridStringId.LayoutViewLabelShowFieldBorder: return "Kenar Göster";
                case GridStringId.MenuGroupPanelFullCollapse: return "Tamamen daralt";
                case GridStringId.LayoutViewGroupHiddenItems: return "Gizli Nesneler";
                case GridStringId.LayoutViewPageTemplateCard: return "Şablon Kartı";
                case GridStringId.MenuColumnBestFit: return "En uygun";
                case GridStringId.LayoutViewPageViewLayout: return "Görünüm Düzeni";
                case GridStringId.GridGroupPanelText: return "Bir kolona göre gruplamak için o kolonun başlığını buraya taşı";
                case GridStringId.LayoutViewLabelHorizontal: return "Yatay Aralık";
                case GridStringId.PrintDesignerDescription: return "Geçerli görünüm için değişik yazdırma seçenekleri kur.";
                case GridStringId.CustomFilterDialogOkButton: return "&Tamam";
                case GridStringId.LayoutViewCustomizationFormDescription: return "Kart düzenini ve özelleştirme menüsünü taşıyıp bırakarak özelleştir. Veriyi Görünüm Düzeni sayfasında önizle.";
                case GridStringId.CustomFilterDialogConditionLike: return "benzer";
                case GridStringId.LayoutViewLabelViewMode: return "Görünüm Biçimi:";
                case GridStringId.CustomFilterDialogConditionGTE: return "büyük ya da eşittir";
                case GridStringId.CustomFilterDialogConditionEQU: return "eşittir";
                case GridStringId.CustomFilterDialogConditionLTE: return "küçük ya da eşittir";
                case GridStringId.CustomFilterDialogConditionNEQ: return "eşit değildir";
                case GridStringId.MenuColumnUnGroup: return "Grubu Dağıt";
                case GridStringId.LayoutViewLabelShowFieldHint: return "İpucu Göster";
                case GridStringId.LayoutViewColumnModeBtnHint: return "Bir Kolon";
                case GridStringId.LayoutViewGroupView: return "Görünüm";
                case GridStringId.LayoutViewButtonShrinkToMinimum: return "Şablon Kartını Kü&çült";
                case GridStringId.CustomFilterDialogConditionBlanks: return "boş alanlar";
                case GridStringId.LayoutViewGroupIndents: return "Girintiler";
                case GridStringId.PrintDesignerCardView: return "Yazdırma Seçenekleri";
                case GridStringId.LayoutViewButtonOk: return "&Tamam";
                case GridStringId.MenuFooterCustomFormat: return "Özel{0}";
                case GridStringId.MenuColumnGroupBox: return "Kutuya göre Grupla";
                case GridStringId.CustomFilterDialogConditionGT: return "büyüktür";
                case GridStringId.CustomFilterDialogConditionLT: return "küçüktür";
                case GridStringId.LayoutViewCustomizationFormCaption: return "Düzen Görünümü Özelleştirmesi";
                case GridStringId.LayoutViewMultiColumnModeBtnHint: return "Birçok Kolon";
                case GridStringId.MenuColumnGroup: return "Bu Kolona göre Grupla";
                case GridStringId.MenuFooterCountFormat: return "{0}";
                case GridStringId.LayoutViewRowModeBtnHint: return "Bir Satır";
                case GridStringId.MenuFooterCount: return "Adet";
                case GridStringId.PopupFilterAll: return "(Hepsi)";
                case GridStringId.CardViewNewCard: return "Yeni Kart";
                case GridStringId.ColumnViewExceptionMessage: return "Değeri düzeltmek ister misin?";
                case GridStringId.MenuColumnBestFitAllColumns: return "En uygun (tüm kolonlar)";
                case GridStringId.MenuFooterNone: return "Hiçbiri";
                case GridStringId.MenuColumnClearFilter: return "Filtreyi Temizle";
                case GridStringId.PopupFilterBlanks: return "(Boş alanlar)";
                case GridStringId.LayoutViewLabelShowCardBorder: return "Kenar Göster";
                case GridStringId.MenuFooterCountGroupFormat: return "Adet{0}";
                case GridStringId.PopupFilterCustom: return "(Özel)";
                case GridStringId.LayoutViewCloseZoomBtnHintZoom: return "Detayı Çoğalt";
                case GridStringId.LayoutViewMultiRowModeBtnHint: return "Birçok Satır";
                case GridStringId.MenuGroupPanelFullExpand: return "Tamamen genişlet";
                case GridStringId.LayoutViewButtonCustomizeHide: return "Özelleştirmeyi &Gizle";
                case GridStringId.LayoutViewButtonCustomizeShow: return "Özelleştirmeyi G&öster";
                case GridStringId.LayoutViewGroupIntervals: return "Aralıklar";
                case GridStringId.LayoutViewButtonApply: return "&Uygula";
                case GridStringId.CardViewQuickCustomizationButtonSort: return "Sırala:";
                case GridStringId.CustomFilterDialogConditionNonBlanks: return "boş olmayan alanlar";
                case GridStringId.LayoutViewButtonReset: return "Şablon Kartını &Sıfırla";
                case GridStringId.MenuGroupPanelClearGrouping: return "Gruplamayı Temizle";
                case GridStringId.PrintDesignerLayoutView: return "Yazdırma Seçenekleri (Düzen Görünümü)";
                case GridStringId.MenuColumnColumnCustomization: return "Kolon Seçici";
                case GridStringId.MenuFooterMaxFormat: return "MAKS{0}";
                case GridStringId.MenuFooterAverage: return "Ortalama";
                case GridStringId.LayoutViewGroupFields: return "Alanlar";
                case GridStringId.CustomFilterDialog2FieldCheck: return "Alan";
                case GridStringId.CardViewQuickCustomizationButton: return "Özelleştir";
                case GridStringId.LayoutViewLabelGroupCaptionLocation: return "Baş Yer Gruplama:";
                case GridStringId.LayoutViewCustomizeBtnHint: return "Özelleştirme";
                case GridStringId.FileIsNotFoundError: return "{0} dosyası bulunamadı";
                case GridStringId.LayoutViewLabelTextIndent: return "Metin Girintileri";
                case GridStringId.LayoutViewLabelVertical: return "Dikey Aralık";
                case GridStringId.LayoutViewGroupLayout: return "Düzen";
                case GridStringId.FilterPanelCustomizeButton: return "Filtreyi Düzenle";
                case GridStringId.CustomFilterDialogRadioAnd: return "&Ve";
                case GridStringId.LayoutViewButtonLoadLayout: return "Düzeni &Yükle...";
                case GridStringId.LayoutViewLabelCardArrangeRule: return "Kural Düzenle";
                case GridStringId.CustomFilterDialogCaption: return "Sıraları göster:";
                case GridStringId.LayoutViewPanBtnHint: return "Kaydırarak";
                case GridStringId.PrintDesignerBandedView: return "Yazdırma Seçenekleri";
                case GridStringId.LayoutViewGroupCaptions: return "Başlık";
                case GridStringId.MenuFooterMax: return "Maks";
                case GridStringId.MenuFooterMin: return "Min";
                case GridStringId.MenuFooterSum: return "Toplam";
                case GridStringId.LayoutViewSingleModeBtnHint: return "Bir Kart";
                case GridStringId.LayoutViewLabelCaptionLocation: return "Alan Başlığı Yeri:";
                case GridStringId.MenuColumnSortDescending: return "Azalarak Sıralat";
                case GridStringId.MenuFooterAverageFormat: return "ORT{0:#-##}";
                case GridStringId.GridOutlookIntervals: return "Daha Eski;Geçen Ay;Bu Ayın Başında;Üç Hafta Önce;İki Hafta Önce;Geçen Hafta;;;;;;;;;;Dün;Bugün;Yarın;;;;;;;;;;Haftaya;İki Hafta Sonra;Üç Hafta Sonra;Bu Ayın Sonunda;Öbür Ayda;Öbür Ayın İllerisinde;";
                case GridStringId.LayoutViewLabelSpacing: return "Aralık";
                case GridStringId.CustomFilterDialogClearFilter: return "Filtreyi &Sıfırla";
                case GridStringId.MenuFooterSumFormat: return "TOPLAM{0:#-##}";
                case GridStringId.LayoutViewButtonPreview: return "Daha &Fazla Kart Göster";
                case GridStringId.MenuColumnFilter: return "Filtre edebilir";
                case GridStringId.MenuFooterMinFormat: return "MIN{0}";
                case GridStringId.LayoutViewGroupCards: return "Kartlar";
                case GridStringId.CustomizationCaption: return "Özelleştirme";
                case GridStringId.LayoutModifiedWarning: return "Düzen değişti. Değişiklikleri kaydetmek ister misin?";
                case GridStringId.LayoutViewButtonCancel: return "&İptal";
                case GridStringId.PrintDesignerGridView: return "Yazdırma Seçenekleri";
                case GridStringId.FilterBuilderApplyButton: return "&Uygula";
                case GridStringId.LayoutViewLabelShowHeaderPanel: return "Başlık Panelini Göster";
                case GridStringId.CardViewQuickCustomizationButtonFilter: return "Filtre";
                case GridStringId.CustomFilterDialogConditionNotLike: return "benzemez";
                case GridStringId.CustomizationFormColumnHint: return "Düzeni özelleştirmek için kolonları buraya taşı bırak";
                case GridStringId.LayoutViewGroupCustomization: return "Özelleştirme";
                case GridStringId.LayoutViewButtonSaveLayout: return "Düzeni &Kaydet...";
                case GridStringId.LayoutViewCloseZoomBtnHintClose: return "Görünümü Geri Yükle";
                case GridStringId.CustomizationColumns: return "Kolonlar";
                case GridStringId.LayoutViewLabelShowLines: return "Satırları Göster";
                case GridStringId.FilterBuilderOkButton: return "&Tamam";
                case GridStringId.MenuColumnFilterEditor: return "Filtre Düzenleyici";
                case GridStringId.LayoutViewLabelShowFilterPanel: return "Filtre Panelini Göster";
            }
            return id.ToString();
        }
    }

    public class LayoutLocalizerTr : LayoutLocalizer
    {
        public override string GetLocalizedString(LayoutStringId id)
        {
            switch (id)
            {
                case LayoutStringId.LayoutItemDescription: return "Kontrol öğesi Elemanlarını Düzenle";
                case LayoutStringId.LockWidthMenuItem: return "Genişliği Kilitle";
                case LayoutStringId.TextPositionBottomMenuText: return "Alt";
                case LayoutStringId.EmptyTabbedGroupText: return "Grupları Sekmeli Grup Alanı içine sürükle ve bırak";
                case LayoutStringId.RenameMenuText: return "Yeniden isimlendir";
                case LayoutStringId.LockHeightMenuItem: return "Yükseltiyi kilitle";
                case LayoutStringId.EmptySpaceItemDefaultText: return "Boş Yer Öğesi";
                case LayoutStringId.TreeViewRootNodeName: return "Kök";
                case LayoutStringId.RedoHintCaption: return "Tekrarla(Ctrl+Y)";
                case LayoutStringId.UnLockItemSizeMenuText: return "Öğe ebatı kilidini Aç";
                case LayoutStringId.LockMenuGroup: return "Sınırlamaları İstenilen ebata ayarla";
                case LayoutStringId.TextPositionRightMenuText: return "Sağ";
                case LayoutStringId.UnGroupItemsMenuText: return "Grubu Çöz";
                case LayoutStringId.TextPositionTopMenuText: return "Üst";
                case LayoutStringId.ShowTextMenuItem: return "Metni Göster";
                case LayoutStringId.HiddenItemsPageTitle: return "Saklı Öğeler";
                case LayoutStringId.FreeSizingMenuItem: return "Serbest Ebatlandırma";
                case LayoutStringId.UnGroupTabbedGroupMenuText: return "Sekmeli grubu Çöz";
                case LayoutStringId.AddTabMenuText: return "Sekme Ekle";
                case LayoutStringId.LockSizeMenuItem: return "Ebatı Kilitle";
                case LayoutStringId.UndoButtonHintText: return "Son Aksiyonu geri Al";
                case LayoutStringId.ControlGroupDefaultText: return "Grup";
                case LayoutStringId.CustomizationParentName: return "Uyarlama";
                case LayoutStringId.LoadButtonHintText: return "XML dosyasından düzenlemeleri yükle";
                case LayoutStringId.TextPositionMenuText: return "Metin Pozisyonu";
                case LayoutStringId.TextPositionLeftMenuText: return "Sol";
                case LayoutStringId.RenameSelected: return "Yeniden isimlendir";
                case LayoutStringId.SaveHintCaption: return "Kaydet (Ctrl+S)";
                case LayoutStringId.LayoutControlDescription: return "Kontrolü Düzenle";
                case LayoutStringId.GroupItemsMenuText: return "Grup";
                case LayoutStringId.LayoutGroupDescription: return "Kontrol Grup Elemanlarını Düzenle";
                case LayoutStringId.LockItemSizeMenuText: return "Öğe Ebatını Kilitle";
                case LayoutStringId.TreeViewPageTitle: return "Ağaç Görüntüsü Düzenle";
                case LayoutStringId.DefaultEmptyText: return "Hiçbiri";
                case LayoutStringId.UndoHintCaption: return "Geri Al (Ctrl+Z)";
                case LayoutStringId.ResetConstraintsToDefaultsMenuItem: return "Varsayımı yeniden başlat";
                case LayoutStringId.DefaultItemText: return "Öğe";
                case LayoutStringId.RedoButtonHintText: return "Son aksiyonu Tekrarla";
                case LayoutStringId.SplitterItemDefaultText: return "Bölümlendirici";
                case LayoutStringId.ResetLayoutMenuText: return "Düzenlemeyi Yeniden başlat";
                case LayoutStringId.ShowCustomizationFormMenuText: return "Düzenlemeyi Uyarla";
                case LayoutStringId.DefaultActionText: return "Varsayım Hareketi";
                case LayoutStringId.SaveButtonHintText: return "Düzenlemeleri XML dosyasına kaydet";
                case LayoutStringId.CreateEmptySpaceItem: return "Boş Yer Yarat";
                case LayoutStringId.HideTextMenuItem: return "Metni Sakla";
                case LayoutStringId.LoadHintCaption: return "Yükle (Ctrl+O)";
                case LayoutStringId.EmptyRootGroupText: return "Kontrolü Buraya Sürükle";
                case LayoutStringId.CreateTabbedGroupMenuText: return "Sekmeli Grup Yarat";
                case LayoutStringId.CustomizationFormTitle: return "Uyarlama";
                case LayoutStringId.TabbedGroupDescription: return "Sekmeli Grup elemanları kontrolünü düzenle";
                case LayoutStringId.HideItemMenutext: return "Öğeyi Sakla";
                case LayoutStringId.HideCustomizationFormMenuText: return "Uyarlama Formunu Sakla";
            }
            return id.ToString();
        }
    }

    public class NavBarLocalizerTr : NavBarLocalizer
    {
        public override string GetLocalizedString(NavBarStringId id)
        {
            switch (id)
            {
                case NavBarStringId.NavPaneMenuAddRemoveButtons: return "Tuşları ekle/Kaldır";
                case NavBarStringId.NavPaneMenuShowMoreButtons: return "Daha Çok butonları göster";
                case NavBarStringId.NavPaneChevronHint: return "Butonları konfigüre et!";
                case NavBarStringId.NavPaneMenuShowFewerButtons: return "Daha az butonları Göster";
            }
            return id.ToString();
        }
    }

    public class LocalizerTr : Localizer
    {
        public override string GetLocalizedString(StringId id)
        {
            switch (id)
            {
                case StringId.FilterToolTipNodeAction: return "İşlemler";
                case StringId.PictureEditOpenFileFilter: return "Bitmap Dosyaları (*.bmp)|*.bmp|GIF Dosyaları (*.gif)|*.gif|JPEG Dosyaları (*.jpg;*.jpeg)|*.jpg;*.jpeg|İkon Dosyaları (*.ico)|*.ico|Tüm Resim Dosyaları |*.bmp;*.gif;*.jpg;*.jpeg;*.ico;*.png;*.tif|Tüm Dosyalar |*.*";
                case StringId.FilterCriteriaToStringBinaryOperatorEqual: return "= (Eşittir)";
                case StringId.FilterMenuConditionAdd: return "Koşul Ekle";
                case StringId.FilterCriteriaToStringBinaryOperatorMinus: return "- (Eksi)";
                case StringId.NavigatorNextButtonHint: return "İleri";
                case StringId.FilterCriteriaToStringBinaryOperatorLike: return "Benzeyen";
                case StringId.FilterCriteriaToStringBinaryOperatorLess: return "< (Küçüktür)";
                case StringId.FilterCriteriaToStringBinaryOperatorPlus: return "+ (Artı)";
                case StringId.FilterClauseGreater: return "Büyüktür";
                case StringId.ImagePopupPicture: return "(Resim)";
                case StringId.FilterClauseLessOrEqual: return "Küçük Eşittir";
                case StringId.FilterClauseEndsWith: return "İle Biten";
                case StringId.TransparentBackColorNotSupported: return "Bu kontrol transparan arka plan desteklememektedir";
                case StringId.TabHeaderButtonNext: return "Sağa Kaydır";
                case StringId.TabHeaderButtonPrev: return "Sola Kaydır";
                case StringId.XtraMessageBoxOkButtonText: return "&Tamam";
                case StringId.ContainerAccessibleEditName: return "Düzenleme Kontrolü";
                case StringId.FilterToolTipNodeRemove: return "Bu koşulu kaldır";
                case StringId.FilterCriteriaToStringUnaryOperatorIsNull: return "Boş Olan";
                case StringId.PreviewPanelText: return "Önizleme";
                case StringId.FilterClauseNoneOf: return "Hiçbiri Olmayan";
                case StringId.FilterClauseIsNull: return "Boş Olan";
                case StringId.Cancel: return "İptal";
                case StringId.FilterCriteriaToStringFunctionLower: return "Küçük Harfe Çevir";
                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalLastWeek: return "Geçen Haftası";
                case StringId.FilterCriteriaToStringFunctionUpper: return "Büyük Harfe Çevir";
                case StringId.FilterCriteriaToStringUnaryOperatorMinus: return "Eksi";
                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalToday: return "Bugünü";
                case StringId.FilterClauseNotLike: return "Benzemeyen";
                case StringId.FilterClauseEquals: return "Eşittir";
                case StringId.DateEditToday: return "Bugün";
                case StringId.DateEditClear: return "Temizle";
                case StringId.PictureEditMenuCut: return "Kes";
                case StringId.NavigatorEditButtonHint: return "Düzenle";
                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalNextWeek: return "Gelecek Haftası";
                case StringId.FilterToolTipKeysRemove: return "(Kes yada Sil Anahtarı kullan)";
                case StringId.FilterCriteriaToStringBinaryOperatorNotEqual: return "<> (Eşit Olmayan)";
                case StringId.FilterGroupNotAnd: return "Değil ve";
                case StringId.FilterGroupOr: return "veya";
                case StringId.FilterMenuClearAll: return "Hepsini Sil";
                case StringId.TextEditMenuCut: return "Ke&s";
                case StringId.FilterCriteriaToStringGroupOperatorOr: return "veya";
                case StringId.ImagePopupEmpty: return "(Boş)";
                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalEarlierThisWeek: return "Bu Haftanın Öncesi";
                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalEarlierThisYear: return "Bu yılın Öncesi";
                case StringId.NavigatorNextPageButtonHint: return "Sonraki Sayfa";
                case StringId.NavigatorTextStringFormat: return "Kayıt {0} / {1}";
                case StringId.Apply: return "Uygula";
                case StringId.FilterDateTimeOperatorMenuCaption: return "Tarih,Saat Operatörleri";
                case StringId.CaptionError: return "Hata";
                case StringId.XtraMessageBoxNoButtonText: return "&Hayır";
                case StringId.PictureEditOpenFileTitle: return "Aç";
                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalEarlierThisMonth: return "Bu Ayın Öncesi";
                case StringId.PictureEditOpenFileError: return "Yanlış resim formatı";
                case StringId.FilterMenuRowRemove: return "Satır Kaldır";
                case StringId.FilterCriteriaToStringBetween: return "Arasında";
                case StringId.XtraMessageBoxIgnoreButtonText: return "Y&oksay";
                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalTomorrow: return "Yarını";
                case StringId.NavigatorRemoveButtonHint: return "Sil";
                case StringId.FilterGroupAnd: return "ve";
                case StringId.FilterClauseLess: return "Küçüktür";
                case StringId.FilterClauseLike: return "Benzeyen";
                case StringId.TabHeaderButtonClose: return "Kapat";
                case StringId.FilterCriteriaToStringBinaryOperatorMultiply: return "* (Çarpı)";
                case StringId.FilterMenuGroupAdd: return "Grup Ekle";
                case StringId.CheckUnchecked: return "Seçim Yok";
                case StringId.FilterCriteriaToStringFunctionNone: return "Hiçbiri";
                case StringId.FilterCriteriaToStringFunctionTrim: return "Boşlukları Al";
                case StringId.PictureEditSaveFileFilter: return "Bitmap Dosyaları (*.bmp)|*.bmp|GIF Dosyaları (*.gif)|*.gif|JPEG Dosyaları (*.jpg)|*.jpg";
                case StringId.TextEditMenuSelectAll: return "Tümü&nü Seç";
                case StringId.PictureEditSaveFileTitle: return "Farklı Kaydet";
                case StringId.FilterShowAll: return "(Hepsini Göster)";
                case StringId.FilterClauseDoesNotContain: return "İçermeyen";
                case StringId.FilterToolTipElementAdd: return "Listeye Yeni Bir Eleman Ekle";
                case StringId.DataEmpty: return "Resim yok";
                case StringId.XtraMessageBoxAbortButtonText: return "&İptal";
                case StringId.CheckIndeterminate: return "Belirsiz";
                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalBeyondThisYear: return "Bu Yılın Ötesi";
                case StringId.FilterClauseBetweenAnd: return "Ve";
                case StringId.NavigatorLastButtonHint: return "Son";
                case StringId.FilterCriteriaToStringFunctionSubstring: return "Parçala";
                case StringId.TextEditMenuCopy: return "&Kopyala";
                case StringId.TextEditMenuUndo: return "&Geri Al";
                case StringId.CalcError: return "Hesaplama Hatası";
                case StringId.FilterCriteriaToStringBinaryOperatorBitwiseXor: return "^";
                case StringId.FilterCriteriaToStringBinaryOperatorBitwiseAnd: return "& (Ve)";
                case StringId.FilterCriteriaToStringUnaryOperatorPlus: return "Artı";
                case StringId.FilterCriteriaToStringFunctionLen: return "Uzunluk";
                case StringId.FilterCriteriaToStringFunctionIif: return "Iif";
                case StringId.CalcButtonBack: return "Geri";
                case StringId.CalcButtonSqrt: return "sqrt";
                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalPriorThisYear: return "Öncelikle Bu Yılı";
                case StringId.LookUpColumnDefaultName: return "İsim";
                case StringId.NavigatorEndEditButtonHint: return "Düzenlemeyi Bitir";
                case StringId.FilterClauseContains: return "İçeren";
                case StringId.NotValidArrayLength: return "Yanlış dizi uzunluğu";
                case StringId.ColorTabWeb: return "Internet";
                case StringId.PictureEditMenuSave: return "Kaydet";
                case StringId.PictureEditMenuCopy: return "Kopyala";
                case StringId.PictureEditMenuLoad: return "Aç";
                case StringId.NavigatorFirstButtonHint: return "İlk";
                case StringId.FilterCriteriaToStringBinaryOperatorDivide: return "/ (Bölü)";
                case StringId.MaskBoxValidateError: return "Girilen değer tamamlanmamış.  Doğrulamak ister misiniz?\n\n\n	Evet - Editöre dön ve değeri doğrula.\n	Hayır - Değeri olduğu gibi bırak.\n	Vazgeç - Önceki değere dön.\n";
                case StringId.FilterClauseBetween: return "Arasında";
                case StringId.FilterCriteriaToStringBinaryOperatorModulo: return "% (Yüzde)";
                case StringId.FilterCriteriaToStringBinaryOperatorLessOrEqual: return "<= (Küçük Eşittir)";
                case StringId.UnknownPictureFormat: return "Bilinmeyen resim formatı";
                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalLaterThisYear: return "Bu Yılın Sonrası";
                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalLaterThisWeek: return "Bu Haftanın Sonrası";
                case StringId.NavigatorPreviousPageButtonHint: return "Önceki Sayfa";
                case StringId.FilterClauseNotBetween: return "Arasında Olmayan";
                case StringId.FilterEmptyEnter: return "Veri Giriniz";
                case StringId.FilterEmptyValue: return "Boş";
                case StringId.XtraMessageBoxRetryButtonText: return "Yeniden &Dene";
                case StringId.FilterToolTipKeysAdd: return "(Araya yada sona anahtar ekle)";
                case StringId.FilterClauseGreaterOrEqual: return "Büyük Eşittir";
                case StringId.LookUpEditValueIsNull: return "[Değer Boş]";
                case StringId.CalcButtonC: return "C";
                case StringId.XtraMessageBoxCancelButtonText: return "&Vazgeç";
                case StringId.FilterGroupNotOr: return "Değil veya";
                case StringId.FilterCriteriaToStringUnaryOperatorBitwiseNot: return "~";
                case StringId.LookUpInvalidEditValueType: return "Yanlış LookUpEdit EditValue tipi.";
                case StringId.NavigatorAppendButtonHint: return "Eklendi";
                case StringId.CalcButtonMx: return "M+";
                case StringId.CalcButtonMC: return "MC";
                case StringId.CalcButtonMS: return "MS";
                case StringId.CalcButtonMR: return "MR";
                case StringId.CalcButtonCE: return "CE";
                case StringId.NavigatorCancelEditButtonHint: return "Düzeltme İptal";
                case StringId.FilterToolTipValueType: return "Bir değer ile veya başka bir alanın değeri ile karşılaştır";
                case StringId.FilterCriteriaToStringFunctionIsNull: return "Boş Olan";
                case StringId.FilterCriteriaToStringBinaryOperatorBitwiseOr: return "| (Veya)";
                case StringId.FilterCriteriaToStringGroupOperatorAnd: return "ve";
                case StringId.FilterClauseDoesNotEqual: return "Farklıdır";
                case StringId.FilterCriteriaToStringBinaryOperatorGreater: return "> (Büyüktür)";
                case StringId.PictureEditOpenFileErrorCaption: return "Açma hatası";
                case StringId.OK: return "Tamam";
                case StringId.FilterToolTipNodeAdd: return "Bu gruba yeni bir koşul ekle";
                case StringId.PictureEditCopyImageError: return "Resim kopyalanamıyor";
                case StringId.CheckChecked: return "Seçili";
                case StringId.FilterCriteriaToStringUnaryOperatorNot: return "Değil";
                case StringId.FilterCriteriaToStringNotLike: return "Benzemeyen";
                case StringId.TextEditMenuPaste: return "&Yapıştır";
                case StringId.TextEditMenuDelete: return "Si&l";
                case StringId.FilterClauseBeginsWith: return "İle Başlayan";
                case StringId.ColorTabSystem: return "Sistem";
                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalLaterThisMonth: return "Bu Ayın Sonrası";
                case StringId.PictureEditMenuPaste: return "Yapıştır";
                case StringId.FilterOutlookDateText: return "Tümünü Göster|Tarihe Göre Filtrele:|Bu Yılın Ötesi|Bu Yıldan Sonra|Bu Aydan Sonra|Gelecek Hafta|Bu Haftadan Sonra|Yarın|Bugün|Dün|Bu Haftadan Önce|Geçen Hafta|Bu Aydan Önce|Bu Yıldan Önce|Öncelikle Bu yıl";
                case StringId.FilterCriteriaToStringFunctionIsOutlookIntervalYesterday: return "Dünü";
                case StringId.FilterCriteriaToStringFunctionCustom: return "Özelleştir";
                case StringId.FilterCriteriaToStringIn: return "İçinde";
                case StringId.FilterClauseIsNotNull: return "Boş Olmayan";
                case StringId.XtraMessageBoxYesButtonText: return "&Evet";
                case StringId.InvalidValueText: return "Yanlış Değer";
                case StringId.FilterClauseAnyOf: return "Herhangi Biri";
                case StringId.FilterCriteriaToStringBinaryOperatorGreaterOrEqual: return ">= (Büyük Eşittir)";
                case StringId.PictureEditMenuDelete: return "Sil";
                case StringId.NavigatorPreviousButtonHint: return "Önceki";
                case StringId.ColorTabCustom: return "Özel";
                case StringId.FilterCriteriaToStringIsNotNull: return "Boş Olmayan";
            }
            return id.ToString();
        }
    }
}