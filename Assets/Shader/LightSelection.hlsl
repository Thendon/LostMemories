#ifndef LIGHTSELECTION_INCLUDED
#define LIGHTSELECTION_INCLUDED

void LightSelection_float(float value, Gradient c0, Gradient c1, Gradient c2, Gradient c3, out Gradient Out) {
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
	default:
		Out = c3;
		break;
	}
}

bool isfinite(Gradient g)
{
	return true;
}
#endif