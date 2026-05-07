mergeInto(LibraryManager.library, {

  MostrarDialogoImpresion: function(ptrBase64) {
    var base64 = UTF8ToString(ptrBase64);

    var iframe = document.createElement('iframe');
    iframe.style.cssText = 'position:fixed;width:0;height:0;border:0;left:-9999px;top:-9999px;visibility:hidden;';
    document.body.appendChild(iframe);

    var doc = iframe.contentDocument || iframe.contentWindow.document;

    var html = [
      '<!DOCTYPE html>',
      '<html><head><title></title>',
      '<style>',
        '@page { size: A4 landscape; margin: 0; }',
        '* { margin:0; padding:0; box-sizing:border-box; }',
        'html, body { width:100%; height:100%; background:#fff; }',
        '.wrap { width:100%; height:100%; display:flex; align-items:center; justify-content:center; }',
        'img { display:block; max-width:100%; max-height:100%; width:auto; height:auto; }',
        '@media print {',
          'html, body { margin:0; padding:0; }',
          '.wrap { width:100vw; height:100vh; }',
          'img { page-break-inside:avoid; }',
        '}',
      '</style>',
      '</head><body>',
        '<div class="wrap">',
          '<img id="pi" src="data:image/jpeg;base64,', base64, '"/>',
        '</div>',
      '</body></html>'
    ].join('');

    doc.open();
    doc.write(html);
    doc.close();

    var img = doc.getElementById('pi');

    img.onload = function() {
      iframe.contentWindow.focus();
      iframe.contentWindow.print();

      setTimeout(function() {
        if (iframe.parentNode) {
          iframe.parentNode.removeChild(iframe);
        }
      }, 3000);
    };

    img.onerror = function() {
      console.error('[PrintPlugin] No se pudo cargar la imagen.');
      if (iframe.parentNode) {
        iframe.parentNode.removeChild(iframe);
      }
    };
  }

});