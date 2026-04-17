mergeInto(LibraryManager.library, {

  MostrarDialogoImpresion: function(ptrBase64) {
    var base64 = UTF8ToString(ptrBase64);

    // Inyectar un iframe oculto con la imagen y abrir el diálogo
    // de configuración del navegador (NO imprime en automático)
    var iframe = document.createElement('iframe');
    iframe.style.cssText = 'position:fixed;width:0;height:0;border:0;' +
                           'left:-9999px;top:-9999px;visibility:hidden;';
    document.body.appendChild(iframe);

    var doc = iframe.contentDocument || iframe.contentWindow.document;
    doc.open();
    doc.write(
      '<!DOCTYPE html>' +
      '<html><head><title>Imprimir</title>' +
      '<style>' +
        '* { margin:0; padding:0; box-sizing:border-box; }' +
        'body { background:#fff; }' +
        'img  { display:block; width:100%; height:auto; }' +
        '@media print {' +
          'body { margin:0; }' +
          'img  { width:100%; page-break-inside:avoid; }' +
        '}' +
      '</style>' +
      '</head>' +
      '<body>' +
        '<img id="pi" src="data:image/jpeg;base64,' + base64 + '"/>' +
      '</body></html>'
    );
    doc.close();

    var img = doc.getElementById('pi');

    img.onload = function() {
      iframe.contentWindow.focus();

      // window.print() abre el panel de configuración del navegador.
      // El usuario elige impresora, páginas, escala, etc.
      // y decide si confirmar o cancelar — nunca imprime solo.
      iframe.contentWindow.print();

      setTimeout(function() {
        if (iframe.parentNode)
          iframe.parentNode.removeChild(iframe);
      }, 3000);
    };

    img.onerror = function() {
      console.error('[PrintPlugin] No se pudo cargar la imagen.');
      if (iframe.parentNode)
        iframe.parentNode.removeChild(iframe);
    };
  }

});