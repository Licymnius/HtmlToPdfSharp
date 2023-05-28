#pragma once
#include "pdf.h"

namespace wk_html_to_pdf_wrapper
{	
	public ref class wkhtmltopdf_wrapper sealed
	{
		wkhtmltopdf_global_settings* global_settings;
		wkhtmltopdf_object_settings* object_settings;		

	public:
		delegate void MessageDelegate(System::String^);
		delegate void ProgressDelegate(int);
		event MessageDelegate^ ErrorEvent;
		event MessageDelegate^ WarningEvent;
		event ProgressDelegate^ ProgressEvent;
		event ProgressDelegate^ FinishedEvent;
		event MessageDelegate^ PhaseEvent;
		wkhtmltopdf_wrapper();
		~wkhtmltopdf_wrapper();
		static wkhtmltopdf_wrapper^ wrapper;
		void set_global_settings(System::String^ parameter, System::String^ value);
		void set_object_settings(System::String^ parameter, System::String^ value);		
		void convert();
		static void error(const char* error_message);
		static void warning(const char* warning_message);
		static void progress_changed(int percentage);
		static void finished(int result_code);
		static void phase_changed(const char* phase_description);
		static void deinit_wkhtmltopdf();
	};
	
}
