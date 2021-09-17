;
with T as (
	select 
	*,
	ROW_NUMBER() OVER(PARTITION BY a.IdApplication, a.IdEnvironment
                                 ORDER BY a.createDate DESC) AS rk
	from ApplicationVersion a
	where a.IsArchived = 0 /*and a.IdApplication = @IdApplication*/
)
update T
set IsArchived = 1
where rk > 1