#include<iostream.h>
#include<conio.h>
//#include<values.h>    // MAXINT
#include<limits.h>
#define size 16
struct Graph{
char vertex1,vertex2;
int weight;
};
// for Q and result set
struct vertex_set{
 char vertex;
 int dimension;
};
struct PI{
char vertex,parent;
int dimension;
};

class Dijkstra{
struct Graph graph[size];
struct vertex_set q[size],result_set[size];
struct PI pi[size];
int edge,is_directed_graph,vertex,result_set_length,q_length;
// vertex for total vertex and length for pi
// is_directed_graph will help me to understnd about ajdcent of vertices
char source_vertex;
public:
Dijkstra(){
edge=vertex=result_set_length=q_length=0;
is_directed_graph=-1;
source_vertex='\0';
}
void initialize_graph(){
cout<<"Enter few Details: "<<endl;
cout<<"1 for directed graph And 0 for undirected graph: ";
cin>>is_directed_graph;
// while terminates when is_directed_graph either 1 or 0
while(is_directed_graph!=0 && is_directed_graph!=1){
   clrscr();
   cerr<<"for continue the code we have to know is the graph is directed or not"<<endl;
   cout<<"1 for directed graph And 0 for undirected graph: ";
   cin>>is_directed_graph;

}
cout<<"Enter No of edges: "<<endl;
cin>>edge;
cout<<"Enter "<<edge<<" vertex1 and vertex2 with weight"<<endl;

for(int s=0;s<edge;s++){
 cout<<s+1<<":v1,v2 and weight"<<endl;
 cin>>graph[s].vertex1>>graph[s].vertex2>>graph[s].weight;
}
cout<<"plesase Enter Source Vertex: ";
cin>>source_vertex;
}

void DIJKSTRA(){

char u;

 initialize_single_source(source_vertex);
 result_set[0].vertex='\0';
  q_length=vertex;
  // q_length represent's q's elements
 while(q_length>0){

 //extract_min return minimum vertex and add to result set
   u=extract_min(q_length);
   //inform while minimum vertex is removed
   --q_length;

// finding adjcent of u

  for(int s=0;s<edge;s++){

     if(is_directed_graph){
       if(graph[s].vertex1==u &&  is_present(graph[s].vertex2,q_length))
	relex(u,graph[s].vertex2,graph[s].weight);

    }else{
		 // means vertex1 is u then vertex2 it's adj
	  if(graph[s].vertex1==u && is_present(graph[s].vertex2,q_length))
	      relex(u,graph[s].vertex2,graph[s].weight);

	  // means vertex1 its adj
	  else if(graph[s].vertex2==u && is_present(graph[s].vertex1,q_length))
	    relex(u,graph[s].vertex1,graph[s].weight);
     }

   }
 }
show();
}

private:
// *****
void relex(char u,char v,int weight){
int index_u,index_v;

// result set d(u) , q= d(v) + w(u,v) =weight
for(int s=0;s<q_length;s++){
  if(q[s].vertex==v){
     index_v=s;
     break;
  }
}
for(s=0;s<result_set_length;s++){
 if(result_set[s].vertex==u){
    index_u=s;
    break;
 }
}

if(q[index_v].dimension>(result_set[index_u].dimension+weight)){
   q[index_v].dimension=result_set[index_u].dimension+weight;

   for(s=0;s<vertex;s++){
       if(pi[s].vertex==v){
	  pi[s].parent=u;
	  pi[s].dimension=q[index_v].dimension;
	  break;
       }
   }
}

}

char extract_min(int q_length){
int index=0;
int dimension;
char vertex;
 for(int s=1;s<q_length;s++){
   if(q[index].dimension>q[s].dimension){
     index=s;
   }
 }
 dimension=q[index].dimension;
 vertex=q[index].vertex;

 add_result_set(vertex,dimension);

// remove the minimum dimension's vertex and shifting

for(s=index;s<q_length-1;s++){
   q[s]=q[s+1];
}

return vertex;
}

void add_result_set(char vertex,int dimension){
  result_set[result_set_length].vertex=vertex;
  result_set[result_set_length].dimension=dimension;
  result_set_length++;
}

int is_present(char v,int length){
 for(int s=0;s<length;s++){
   if(q[s].vertex==v){
    return 1;
   }
 }
 return 0;
}

void initialize_single_source(char source){

  for(int s=0;s<edge;s++){
    if(!is_present(graph[s].vertex1,vertex)){
       q[vertex].vertex=graph[s].vertex1;
	vertex++;
     }
     if(!is_present(graph[s].vertex2,vertex)){
       q[vertex].vertex=graph[s].vertex2;
       vertex++;
     }
  }
  //initialze the dimension of source and curresponding vertex's dimen
  for(s=0;s<vertex;s++){
     if(q[s].vertex==source){
       q[s].dimension=0;
       // assign vertex and dimension in pi
       pi[s].vertex=q[s].vertex;
       pi[s].dimension=q[s].dimension;
     }else{
      q[s].dimension=INT_MAX;

      pi[s].dimension=q[s].dimension;
      pi[s].vertex=q[s].vertex;
      // no parent vertex
      pi[s].parent='\0';
     }
  }
}
void show(){
cout<<"RESULT SET:"<<endl;
  for(int s=0;s<vertex;s++){
   cout<<result_set[s].vertex<<" "<<result_set[s].dimension<<endl;
  }
 cout<<"PI:"<<endl;
 for(s=0;s<vertex;s++)
   cout<<pi[s].vertex<<" "<<pi[s].parent<<" "<<pi[s].dimension<<endl;

}

};

void main(){
clrscr();
Dijkstra s;
s.initialize_graph();
s.DIJKSTRA();

getch();

}
















