#include<stdio.h>
int cuadrado(int a){
    a = a*a;
    return a;
}
void cuadrado2(int *a){
    (*a) = (*a)*(*a);
    printf("El numero elevado al cuadrado es: %d \n", a);
}
void variable(int *a){
    printf("La direccion de la variable es: %p \n", a);
    printf("El contenido de la variable es: %d \n", *a);
}
void invertir(int *a,int *b){
    int auxiliar = *a;
    *a = *b;
    *b = auxiliar;
}
void orden(int *a, int *b){
    int auxiliar = *b;
    if(*a > *b){
        *b = *a;
        *a = auxiliar;
    }
}
int main(){
    int num;
    printf("Ingrese un numero: \n");
    scanf("%d", &num);
    int numcuad = cuadrado(num);
    printf("El cuadrado del numero ingresado es: %d \n", numcuad);
    cuadrado2(&num);
    variable(&num);
    int num1, num2;
    printf("Ingrese el primer numero: \n");
    scanf("%d", &num1);
    printf("Ingrese el segundo numero: \n");
    scanf("%d", &num2);
    printf("Antes de invertir: \n");
    printf("El numero a es: %d \n", num1);
    printf("El numero b es: %d \n", num2);

    invertir(&num1,&num2);

    printf("Despues de invertir: \n");
    printf("El numero a es: %d \n", num1);
    printf("El numero b es: %d \n", num2);

    orden(&num1,&num2);
    printf("El valor mas chico de a es: %d \n", num1);
    printf("El valor mas grande de b es: %d \n", num2);
    return 0;
}