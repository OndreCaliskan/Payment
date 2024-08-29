(function($) {
    "use strict"

    // $('#example').DataTable();
    
    var table = $('.dataTable').DataTable({
        createdRow: function ( row, data, index ) {
           $(row).addClass('selected')
        } 
        paging: true, 
        pageLength: 10, 
        lengthMenu: [10, 25, 50, 100] 
    });
      
    table.on('click', 'tbody tr', function() {
    var $row = table.row(this).nodes().to$();
    var hasClass = $row.hasClass('selected');
    if (hasClass) {
        $row.removeClass('selected')
    } else {
        $row.addClass('selected')
    }
    })
    
    table.rows().every(function() {
    this.nodes().to$().removeClass('selected')
    });




    $('#example2').DataTable( {
        "scrollY":        "42vh",
        "scrollCollapse": true,
        "paging":         true
    });

    $('#example3').DataTable( {
        "scrollY": "400",
        "scrollX": true
         "paging":         true
    });

    $('#example4').DataTable( {
        "scrollX": true
    });

    $('#example-ajax').DataTable( {
        "ajax": '../ajax/arrays.json'
    } );
    


})(jQuery);

