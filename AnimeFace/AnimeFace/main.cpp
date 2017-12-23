#include <opencv2/opencv.hpp>

int main()
{
	cv::Mat image = cv::imread("../Imgs/Violet_Evergarden.jpg");
	cv::Mat gray = cv::imread("../Imgs/Violet_Evergarden.jpg", cv::IMREAD_GRAYSCALE);
	cv::CascadeClassifier lbp_cascade;
	lbp_cascade.load("lbpcascade_animeface.xml");
	std::vector<cv::Rect_<int> > faces;
	lbp_cascade.detectMultiScale(gray, faces);
	for (int i = 0; i < faces.size(); i++)
	{
		cv::rectangle(image, faces[i], cv::Scalar(0, 255, 0), 2);
	}
	while (image.rows>=1080)
	{
		cv::resize(image, image, cv::Size(image.cols*0.717, image.rows*0.717));
	}
	cv::imshow("Violet_Evergarden", image);
	cv::waitKey();
	return 0;
}