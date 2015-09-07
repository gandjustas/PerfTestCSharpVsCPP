// PerfTestCpp.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
constexpr int N = 10000;


std::chrono::high_resolution_clock::duration measure(std::function<void()> f, int n = 100)
{
	auto begin = std::chrono::high_resolution_clock::now();
	for (int i = 0; i < n; i++)
	{
		f();
	}
	auto end = std::chrono::high_resolution_clock::now();
	return (end - begin) / n;
}


void c_style_sort(int *m, int n) 
{
	for (int i = 0; i < N - 1; i++)
		for (int j = i + 1; j < N; j++) {
			if (m[i] < m[j])
			{
				int tmp = m[i];
				m[i] = m[j];
				m[j] = tmp;
			}
		}
}

void c_style_test()
{
	int* m = new int[N];

	for (int i = 0; i < N; i++)
	{
		m[i] = i;
	}
	c_style_sort(m, N);
	delete[] m;
}

void cpp_style_sort(std::array<int, N> &m)
{
	auto n = m.size();
	for (int i = 0; i < n-1; i++)
		for (int j = i + 1; j < n; j++) {
			if (m[i] < m[j])
			{
				int tmp = m[i];
				m[i] = m[j];
				m[j] = tmp;
			}
		}
}

void cpp_style_test()
{
	std::array<int, N> m;

	for (int i = 0; i < N; i++)
	{
		m[i] = i; 
	}
	cpp_style_sort(m);
}

void vector_sort(std::vector<int> &m)
{
	auto n = m.size();

	for (int i = 0; i < n - 1; i++)
		for (int j = i + 1; j < n; j++) {
			if (m[i] < m[j])
			{
				int tmp = m[i];
				m[i] = m[j];
				m[j] = tmp;
			}
		}
}

void vector_test()
{
	std::vector<int> m;
	m.reserve(N);

	for (int i = 0; i < N; i++)
	{
		m.push_back(i);
	}
	vector_sort(m);
}

int main()
{
	auto m1 = measure(c_style_test);
	std::cout << "C-style array bubble sort = " << std::chrono::duration_cast<std::chrono::milliseconds>(m1).count() << "ms\n";
	auto m2 = measure(cpp_style_test);
	std::cout << "C++ style array bubble sort = " << std::chrono::duration_cast<std::chrono::milliseconds>(m2).count() << "ms\n";
	auto m3 = measure(vector_test);
	std::cout << "C++ vector bubble sort = " << std::chrono::duration_cast<std::chrono::milliseconds>(m3).count() << "ms\n";
	return 0;
}
