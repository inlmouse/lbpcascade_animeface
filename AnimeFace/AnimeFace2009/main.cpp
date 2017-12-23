#include "nv_core.h"
#include "nv_ip.h"
#include "nv_ml.h"
#include "nv_face.h"
#include <opencv2/opencv.hpp>

#define NV_MAX_FACE 1024

int main()
{
	cv::Mat mat = cv::imread("../Imgs/Violet_Evergarden.jpg");
	if (mat.data == nullptr || mat.type() != CV_8UC3)
	{
		std::cout << "image's type is wrong!!Please set CV_8UC3" << std::endl;
		return -1;
	}
	static const nv_mlp_t *detector_mlp = &nv_face_mlp_face_00;
	static const nv_mlp_t *face_mlp[] = {
		&nv_face_mlp_face_01,
		&nv_face_mlp_face_02,
		NULL
	};
	static const nv_mlp_t *dir_mlp = &nv_face_mlp_dir;
	static const nv_mlp_t *parts_mlp = &nv_face_mlp_parts;


	int image_height = mat.rows;
	int image_width = mat.cols;
	nv_rect_t image_size;
	image_size.x = 0;
	image_size.y = 0;
	image_size.width = image_width;
	image_size.height = image_height;
	nv_matrix_t *bgr = nv_matrix3d_alloc(3, image_height, image_width);
	nv_matrix_t *gray = nv_matrix3d_alloc(1, image_height, image_width);
	nv_matrix_t *smooth = nv_matrix3d_alloc(1, image_height, image_width);
	nv_matrix_t *edge = nv_matrix3d_alloc(1, image_height, image_width);
	nv_matrix_t *gray_integral = nv_matrix3d_alloc(1, image_height + 1, image_width + 1);
	nv_matrix_t *edge_integral = nv_matrix3d_alloc(1, image_height + 1, image_width + 1);

	nv_matrix_zero(bgr);
	nv_matrix_zero(gray);
	nv_matrix_zero(edge);
	nv_matrix_zero(gray_integral);
	nv_matrix_zero(edge_integral);
	//

	for (int i = 0; i < image_height; ++i) {
		for (int j = 0; j < image_width; ++j) {
			//const int idx = i * image_width + j;
			NV_MAT3D_V(bgr, i, j, NV_CH_B) = mat.at<cv::Vec3b>(i, j)[2];//rgb_data[idx * 3 + 2];
			NV_MAT3D_V(bgr, i, j, NV_CH_G) = mat.at<cv::Vec3b>(i, j)[1];
			NV_MAT3D_V(bgr, i, j, NV_CH_R) = mat.at<cv::Vec3b>(i, j)[0];
		}
	}
	nv_gray_cpu(gray, bgr);
	nv_gaussian5x5(smooth, 0, gray, 0);
	nv_laplacian1(edge, smooth, 4.0f);
	nv_integral(gray_integral, gray, 0);
	nv_integral(edge_integral, edge, 0);

	nv_face_position_t face_pos[NV_MAX_FACE];
	memset(face_pos, 0x0, sizeof(face_pos));

	float step = 4.0;
	float scale_factor = 1.05;
	float min_window_size = 42.592;

	int nface = nv_face_detect(face_pos, NV_MAX_FACE,
		gray_integral, edge_integral, &image_size,
		dir_mlp,
		detector_mlp, face_mlp, 2,
		parts_mlp,
		step, scale_factor, min_window_size);


	for (int i = 0; i < nface; ++i)
	{
		cv::Rect face_rect(face_pos[i].face.x, face_pos[i].face.y, face_pos[i].face.width, face_pos[i].face.height);
		std::vector<cv::Rect> mark_rects;
		mark_rects.push_back(cv::Rect(face_pos[i].left_eye.x, face_pos[i].left_eye.y, face_pos[i].left_eye.width, face_pos[i].left_eye.height));
		mark_rects.push_back(cv::Rect(face_pos[i].right_eye.x, face_pos[i].right_eye.y, face_pos[i].right_eye.width, face_pos[i].right_eye.height));
		mark_rects.push_back(cv::Rect(face_pos[i].nose.x, face_pos[i].nose.y, face_pos[i].nose.width, face_pos[i].nose.height));
		mark_rects.push_back(cv::Rect(face_pos[i].mouth.x, face_pos[i].mouth.y, face_pos[i].mouth.width, face_pos[i].mouth.height));
		cv::rectangle(mat, face_rect, cv::Scalar(0, 255, 0), 4);
		for (int j = 0; j < mark_rects.size(); j++)
		{
			cv::rectangle(mat, mark_rects[j], cv::Scalar(0, 255, 255), 4);
		}
	}
	while (mat.rows>1080)
	{
		cv::resize(mat, mat, cv::Size(mat.cols / 1.414, mat.rows / 1.414));
	}
	cv::imwrite("Violet_Evergarden.jpg", mat);
	cv::imshow("Violet_Evergarden", mat);
	cv::waitKey();
	return 0;
}