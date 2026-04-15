mergeInto(LibraryManager.library, {
  ImprimirImagen: function(ptrBase64) {
    var base64 = UTF8ToString(ptrBase64);
    var ventana = window.open('', '_blank');
    ventana.document.write(
      '<html><head><title>Imprimir</title>' +
      '<style>body{margin:0;}img{width:100vw;}</style></head>' +
      '<body><img src="data:image/jpeg;base64,' + base64 + '" onload="window.print();window.close();"/></body></html>'
    );
    ventana.document.close();
  }
});