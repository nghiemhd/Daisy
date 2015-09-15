update Album 
set DisplayOrder = T.RowNumber
from (
	select ROW_NUMBER() over (order by Id) as RowNumber, Id from Album
)T
where Album.Id = T.Id