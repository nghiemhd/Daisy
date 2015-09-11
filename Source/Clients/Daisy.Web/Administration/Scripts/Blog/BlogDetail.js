function loadTinymce() {
    tinymce.init({
        mode: "textareas",
        editor_selector: "mceEditor",
        theme: "advanced",
        plugins: "autolink,lists,pagebreak,style,layer,table,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist,autosave,visualblocks",

        // Theme options
        theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
        theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
        theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
        theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak,restoredraft,visualblocks",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: true,
        theme_advanced_resize_horizontal: false,

        theme_advanced_fonts: "Andale Mono=andale mono,times;" +
                "Arial=arial,helvetica,sans-serif;" +
                "Arial Black=arial black,avant garde;" +
                "Book Antiqua=book antiqua,palatino;" +
                "Comic Sans MS=comic sans ms,sans-serif;" +
                "Courier New=courier new,courier;" +
                "Georgia=georgia,palatino;" +
                "Helvetica=helvetica;" +
                "Impact=impact,chicago;" +
                "Open Sans=open sans,helvetica neue,helvetica,arial,sans-serif;" +
                "Symbol=symbol;" +
                "Tahoma=tahoma,arial,helvetica,sans-serif;" +
                "Terminal=terminal,monaco;" +
                "Times New Roman=times new roman,times;" +
                "Trebuchet MS=trebuchet ms,geneva;" +
                "Verdana=verdana,geneva;" +
                "Webdings=webdings;" +
                "Wingdings=wingdings,zapf dingbats",

        content_css: Common.Helper.applicationRoot + "administration/content/tinymce3.css",
    });
}

function countCharacters(maxCharacter, textareaId, labelId)
{
    var text_max = maxCharacter;
    var text_length = $(textareaId).val().length;
    var text_remaining = text_max - text_length;
    $(labelId).html(text_remaining + ' characters remaining');

    $(textareaId).keyup(function () {
        text_length = $(textareaId).val().length;
        text_remaining = text_max - text_length;

        $(labelId).html(text_remaining + ' characters remaining');
    });
}

$(document).ready(function () {
    loadTinymce();

    countCharacters(500, '#txtHighlight', '#textarea_feedback');
    countCharacters(100, '#txtSlug', '#slug_feedback');
    countCharacters(400, '#txtMetaTitle', '#metatitle_feedback');
    countCharacters(400, '#txtMetaKeywords', '#metakeywords_feedback');

    //var text_max = 500;
    //var text_length = $('#txtHighlight').val().length;
    //var text_remaining = text_max - text_length;
    //$('#textarea_feedback').html(text_remaining + ' characters remaining');

    //$('#txtHighlight').keyup(function () {
    //    text_length = $('#txtHighlight').val().length;
    //    text_remaining = text_max - text_length;

    //    $('#textarea_feedback').html(text_remaining + ' characters remaining');
    //});
});

