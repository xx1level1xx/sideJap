#include<iostream>
#include<string>
#include<cstdlib>
using namespace std;

int main(){
	string a = "</tr><tr onMouseOver=\"this.bgColor='#99FF99'\" onMouseOut=\"this.bgColor=''\"><td>Dec 16 - 2:05 PM EST</td><td>Best Buy : Console In Stock for $499.99</td>";
	string b = "";
	for (int i = 0; i < a.length(); i++){
		if (a[i] == '<'){
			b += '(';
		}
		else if (a[i] == '>'){
			b += ')';
		}
		else{
			b += a[i];
		}
	}
	cout << b << endl;
	system("pause");
	return 0;
}