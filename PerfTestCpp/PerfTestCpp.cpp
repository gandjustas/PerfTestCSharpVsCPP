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

template<typename C>
void sort_gen(C& m, int n)
{
	for (int i = 0; i < n - 1; i++)
		for (int j = i + 1; j < n; j++) {
			if (m[i] < m[j])
			{
				std::swap(m[i], m[j]);
			}
		}
}

template<>
void sort_gen(std::vector<int>& m, int n)
{
	for (int i = 0; i < n - 1; i++)
		for (int j = i + 1; j < n; j++) {
			if (m.at(i) < m.at(j))
			{
				std::swap(m[i], m[j]);
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
	sort_gen(m, N);
	delete[] m;
}


void cpp_style_test()
{
	std::array<int, N> m;

	for (int i = 0; i < N; i++)
	{
		m[i] = i; 
	}
	sort_gen(m, m.size());

}


void vector_test()
{
	std::vector<int> m;
	m.reserve(N);

	for (int i = 0; i < N; i++)
	{
		m.push_back(i);
	}
	sort_gen(m, m.size());
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
