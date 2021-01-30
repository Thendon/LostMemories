#ifndef LIGHTSELECTION_INCLUDED
#define LIGHTSELECTION_INCLUDED

void LightSelection_float(float value, Gradient c0, Gradient c1, Gradient c2, Gradient c3, Gradient c4, Gradient c5, out Gradient Out) {
	int tmp = value;
	switch (tmp) {
	case 0:
		Out = c0;
		break;
	case 1:
		Out = c1;
		break;
	case 2:
		Out = c2;
		break;
	case 3:
		Out = c3;
		break;
	case 4:
		Out = c4;
		break;
	default:
		Out = c5;
		break;
	}
}

bool isfinite(Gradient g)
{
	return true;
}
#endif