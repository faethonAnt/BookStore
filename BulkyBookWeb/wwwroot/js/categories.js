$('#tblData').DataTable({
    ajax:'/category/getall',
    columns: [
        {data:"name"},
        {data:"displayOrder"},
        {defaultContent:""}
    ]
})