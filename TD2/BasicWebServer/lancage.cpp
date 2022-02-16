#include <iostream>

int main(int argc, char** argv) {
    //std::cout << "Have " << argc << " arguments:" << std::endl;
    std::cout "<html><body>" << std::endl;
    for (int i = 0; i < argc; ++i) {
        std::cout << "<p>" << argv[i] << "</p>"  << std::endl;
    }
    std::cout " </body></html>" << std::endl;
}