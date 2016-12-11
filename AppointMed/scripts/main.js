$(document).ready(function() {
    var table = $('#example').DataTable();
 
    // Apply the search
    table.columns().every(function () {
        var that = this;
 
        $('input', this.footer()).on( 'keyup change', function () {
            if (that.search() !== this.value ) {
                that.search(this.value)
                    .draw();
            }
        } );
    });
});
