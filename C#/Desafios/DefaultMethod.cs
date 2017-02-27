public static DefaultDTOResponse<T> DefaultMethod<T>(Func<T> action, Action<Exception> tryCatch = null)
{
	var response = new DefaultDTOResponse<T>();

	try
	{
		response.Result = action();
	}
	catch (Exception ex)
	{
		response.IsSucceeded = false;
		response.ErrorMessage = ex.Message;

		tryCatch?.Invoke(ex);
	}

	return response;
}
