#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <pdf.h>
#include "wkhtmltopdfwrapper.h"

namespace wk_html_to_pdf_wrapper
{	
	static const char* convert_string_to_char_array(System::String^ value)
	{
		//convert specified managed String to UTF8 unmanaged string
		auto ptr = System::Runtime::InteropServices::Marshal::StringToHGlobalUni(value);
		const auto wide_char_str_unicode = static_cast<wchar_t*>(ptr.ToPointer());
		const auto utf8_string_size = WideCharToMultiByte(CP_UTF8, 0, wide_char_str_unicode, -1, nullptr, 0, nullptr,
		                                                  nullptr);
		const auto result = new char[utf8_string_size];
		WideCharToMultiByte(CP_UTF8, 0, wide_char_str_unicode, -1, result, utf8_string_size, nullptr, nullptr);
		return result;
	}

	wkhtmltopdf_wrapper::wkhtmltopdf_wrapper()
	{		
		wkhtmltopdf_init(false);		
		global_settings = wkhtmltopdf_create_global_settings();
		object_settings = wkhtmltopdf_create_object_settings();		
	}

	wkhtmltopdf_wrapper::~wkhtmltopdf_wrapper()
	{
		wkhtmltopdf_destroy_global_settings(global_settings);
		wkhtmltopdf_destroy_object_settings(object_settings);
	}

	void wkhtmltopdf_wrapper::set_global_settings(System::String^ parameter, System::String^ value)
	{
		wkhtmltopdf_set_global_setting(global_settings, convert_string_to_char_array(parameter), convert_string_to_char_array(value));
	}

	void wkhtmltopdf_wrapper::set_object_settings(System::String^ parameter, System::String^ value)
	{
		wkhtmltopdf_set_object_setting(object_settings, convert_string_to_char_array(parameter), convert_string_to_char_array(value));
	}	

	void error_cb(wkhtmltopdf_converter*, const char* error_message)
	{
		wkhtmltopdf_wrapper::error(error_message);
	}

	void wkhtmltopdf_wrapper::error(const char* error_message)
	{
		wrapper->ErrorEvent(gcnew System::String(error_message));
	}

	void warning_cb(wkhtmltopdf_converter*, const char* error_message)
	{
		wkhtmltopdf_wrapper::error(error_message);
	}

	void wkhtmltopdf_wrapper::warning(const char* warning_message)
	{
		wrapper->WarningEvent(gcnew System::String(warning_message));
	}

	void progress_cb(wkhtmltopdf_converter*, const int percentage)
	{
		wkhtmltopdf_wrapper::progress_changed(percentage);
	}

	void wkhtmltopdf_wrapper::progress_changed(const int percentage)
	{
		wrapper->ProgressEvent(percentage);
	}	

	void finished_cb(wkhtmltopdf_converter*, const int resultCode)
	{
		wkhtmltopdf_wrapper::finished(resultCode);
	}

	void wkhtmltopdf_wrapper::finished(const int result_code)
	{
		wrapper->FinishedEvent(result_code);
	}	

	void phase_changed_cb(wkhtmltopdf_converter* converter)
	{		
		wkhtmltopdf_wrapper::phase_changed(wkhtmltopdf_phase_description(converter, wkhtmltopdf_current_phase(converter)));
	}
	
	void wkhtmltopdf_wrapper::phase_changed(const char* phase_description)
	{
		wrapper->PhaseEvent(gcnew System::String(phase_description));
	}
		
	void wkhtmltopdf_wrapper::deinit_wkhtmltopdf()
	{
		wkhtmltopdf_deinit();
	}

	/// <summary>
	/// Start conversion process
	/// </summary>
	void wkhtmltopdf_wrapper::convert()
	{
		//Set the object to link callbacks
		wrapper = this;
				
		//Initialize converter
		const auto converter = wkhtmltopdf_create_converter(global_settings);

		try
		{
			//Initialize callbacks
			wkhtmltopdf_set_error_callback(converter, error_cb);
			wkhtmltopdf_set_warning_callback(converter, warning_cb);
			wkhtmltopdf_set_progress_changed_callback(converter, progress_cb);
			wkhtmltopdf_set_finished_callback(converter, finished_cb);
			wkhtmltopdf_set_phase_changed_callback(converter, phase_changed_cb);

			//Set object settings
			wkhtmltopdf_add_object(converter, object_settings, nullptr);

			//Perform conversion
			wkhtmltopdf_convert(converter);				
		}
		finally
		{
			wkhtmltopdf_destroy_converter(converter);
		}		
	}
}

