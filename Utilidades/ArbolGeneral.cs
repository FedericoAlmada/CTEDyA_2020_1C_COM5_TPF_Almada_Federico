using System;
using System.Collections.Generic;

namespace juegoIA
{
	public class ArbolGeneral<T>
	{	
		private NodoGeneral<T> raiz;
		
		public ArbolGeneral(T dato) 
		{
			this.raiz = new NodoGeneral<T>(dato);
		}
	
		private ArbolGeneral(NodoGeneral<T> nodo) 
		{
			this.raiz = nodo;
		}
	
		private NodoGeneral<T> getRaiz() 
		{
			return raiz;
		}
	
		public T getDatoRaiz() 
		{
			return this.getRaiz().getDato();
		}
	
		public List<ArbolGeneral<T>> getHijos() 
		{
			List<ArbolGeneral<T>> temp= new List<ArbolGeneral<T>>();
			foreach (var element in this.raiz.getHijos()) 
			{
				temp.Add(new ArbolGeneral<T>(element));
			}
			
			return temp;
		}
	
		public void agregarHijo(ArbolGeneral<T> hijo) 
		{
			this.raiz.getHijos().Add(hijo.getRaiz());
		}
	
		public void eliminarHijo(ArbolGeneral<T> hijo) 
		{
			this.raiz.getHijos().Remove(hijo.getRaiz());
		}
	
		public bool esVacio() 
		{
			return this.raiz == null;
		}
	
		public bool esHoja() 
		{
			return this.raiz != null && this.getHijos().Count == 0;
		}
	
		public int altura() 
		{
			int alturaMax = -1;
			
			if (this.esHoja())
			{
				return 0;
			}
			else
			{
				foreach (var hijo in this.getHijos())
				{
					int alturaHijo = hijo.altura();
					
					if (alturaHijo > alturaMax)
					{
						alturaMax = alturaHijo;
					}
				}
			}
			
			return alturaMax +1;
		}

		public int nivel(T dato) 
		{
			//En proceso

			return 0;
		}
		
		public void preorden()
		{
			if (!this.esVacio())
			{
				Console.Write(" " + this.getDatoRaiz());
			}
			
			if (!this.esHoja())
			{
				foreach (var hijo in this.getHijos())
				{
					hijo.preorden();
				}
			}
		}
		
		public void postorden()
		{
			if (!this.esHoja())
			{
				foreach (var hijo in this.getHijos())
				{
					hijo.postorden();
				}
			}
			
			if (!this.esVacio())
			{
				Console.Write(" " + this.getDatoRaiz());
			}			
		}

		public void porNiveles()
		{
			Cola<ArbolGeneral<T>> c  = new Cola<ArbolGeneral<T>>();
			ArbolGeneral<T> arbolAux;
		
			c.encolar(this);
			
			while(!c.esVacia())
			{
				arbolAux = c.desencolar();
				
				Console.Write(arbolAux.getDatoRaiz() + " ");
				
				if(!this.esHoja())
				{
					foreach(var hijo in arbolAux.getHijos())
					{
						c.encolar(hijo);
					}
				}
			}			
		}					
	}
}
