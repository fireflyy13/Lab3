using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Lab3TA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Kosaraju_Click(object sender, EventArgs e)
        {
            int v = 11;
            Graph g = new Graph(v);

            g.AddEdge(0, 3);
            g.AddEdge(0, 6);
            g.AddEdge(1, 8);
            g.AddEdge(1, 10);
            g.AddEdge(2, 5);
            g.AddEdge(3, 8);
            g.AddEdge(4, 7);
            g.AddEdge(4, 9);
            g.AddEdge(5, 9);

            g.AddDirEdge(1, 2);
            g.AddDirEdge(4, 2);
            g.AddDirEdge(2, 8);
            g.AddDirEdge(3, 7);
            g.AddDirEdge(10, 5);

            List<List<int>> componentsResult = g.KosarajuPrintComponents(v);
            int length = componentsResult.Count;

            label1.BackColor = Color.Ivory;
            label1.BorderStyle = BorderStyle.FixedSingle;

            foreach (List<int> component in componentsResult)
            {
                StringBuilder componentString = new StringBuilder();
                foreach (int vertex in component)
                    componentString.Append(vertex + 1 + " ");
                componentString.AppendLine($"Number of components: {length}");
                label1.Text = "Обрано: Косараджу\n" + "Компоненти:\n " + 
                    componentString.ToString();

            }
        }

        private void Tarjan_Click(object sender, EventArgs e)
        {
            int v = 11;
            Graph g2 = new Graph(v);

            g2.AddEdge(0, 3);
            g2.AddEdge(0, 6);
            g2.AddEdge(1, 8);
            g2.AddEdge(1, 10);
            g2.AddEdge(2, 5);
            g2.AddEdge(3, 8);
            g2.AddEdge(4, 7);
            g2.AddEdge(4, 9);
            g2.AddEdge(5, 9);

            g2.AddDirEdge(1, 2);
            g2.AddDirEdge(4, 2);
            g2.AddDirEdge(2, 8);
            g2.AddDirEdge(3, 7);
            g2.AddDirEdge(10, 5);

            List<List<int>> componentsResult = g2.TarjanComponents(v);
            int length = componentsResult.Count - 1;

            label1.BackColor = Color.Ivory;
            label1.BorderStyle = BorderStyle.FixedSingle;

            StringBuilder output = new StringBuilder();
            foreach (List<int> component in componentsResult)
            {
                StringBuilder componentString = new StringBuilder();
                foreach (int vertex in component)
                    componentString.Append(vertex + 1 + " ");
                output.Append(componentString.ToString());
            }

            output.AppendLine($"Number of components: {length}");
            label1.Text = "Обрано: Тар'ян\n" + "Компоненти:\n"+ output.ToString();
        }
        public class Graph
        {
            public List<int>[] adj;
            public int time;

            public Graph(int v)
            {
                adj = new List<int>[v];
                for (int i = 0; i < v; ++i)
                    adj[i] = new List<int>();
                time = 0;
            }

            public void AddEdge(int v, int w)
            {
                adj[v].Add(w);
                adj[w].Add(v);
            }

            public void AddDirEdge(int v, int w)
            {
                adj[v].Add(w);
            }

            private void KosarajuDFS(int vertex, bool[] visited, List<int>[] adj, List<int> component)
            {
                visited[vertex] = true;
                component.Add(vertex);

                foreach (int i in adj[vertex])
                {
                    if (!visited[i])
                    {
                        KosarajuDFS(i, visited, adj, component);
                    }
                }
            }

            private void KosarajuOrdering(int vertex, bool[] visited, Stack<int> stack, int v, List<int>[] adj)
            {
                visited[vertex] = true;

                foreach (int i in adj[vertex])
                {
                    if (!visited[i])
                    {
                        KosarajuOrdering(i, visited, stack, v, adj);
                    }
                }

                stack.Push(vertex);
            }

            private Graph KosarajuTranspose(int v)
            {
                Graph g = new Graph(v);

                for (int i = 0; i < v; i++)
                {
                    foreach (int k in adj[i])
                    {
                        g.adj[k].Add(i);
                    }
                }

                return g;
            }

            public List<List<int>> KosarajuPrintComponents(int v)
            {
                List<List<int>> components = new List<List<int>>();

                Stack<int> stack = new Stack<int>();
                bool[] visited = new bool[v];

                for (int i = 0; i < v; i++)
                {
                    if (!visited[i])
                    {
                        KosarajuOrdering(i, visited, stack, v, adj);
                    }
                }

                Graph transposed = KosarajuTranspose(v);

                for (int i = 0; i < v; i++)
                {
                    visited[i] = false;
                }

                while (stack.Count > 0)
                {
                    int vertex = stack.Pop();
                    if (!visited[vertex])
                    {
                        List<int> component = new List<int>();
                        transposed.KosarajuDFS(vertex, visited, adj, component);
                        components.Add(component);
                    }
                }

                return components;
            }

            private void TarjanComponentsUtil(int u, int[] discTime, int[] lowTime, Stack<int> st,
                bool[] stackMember, List<List<int>> components)
            {
                discTime[u] = lowTime[u] = ++time;
                st.Push(u);
                stackMember[u] = true;

                foreach (int v in adj[u])
                {
                    if (discTime[v] == -1)
                    {
                        TarjanComponentsUtil(v, discTime, lowTime, st, stackMember, components);
                        lowTime[u] = Math.Min(lowTime[u], lowTime[v]);
                    }

                    else if (stackMember[v])
                    {
                        lowTime[u] = Math.Min(lowTime[u], discTime[v]);
                    }
                }

                if (lowTime[u] == discTime[u])

                {
                    List<int> component = new List<int>();

                    while (st.Count > 0)
                    {
                        int w = st.Pop();
                        stackMember[w] = false;
                        component.Add(w);

                        if (w == u)
                        {
                            break;
                        }
                    }
                    component.Reverse();
                    components.Add(component);
                }
            }

            public List<List<int>> TarjanComponents(int v)
            {
                int[] discTime = new int[v];
                int[] lowTime = new int[v];
                bool[] stackMember = new bool[v];
                Stack<int> st = new Stack<int>();
                List<List<int>> components = new List<List<int>>();

                for (int i = 0; i < v; i++)
                {
                    discTime[i] = -1;
                    lowTime[i] = -1;
                    stackMember[i] = false;
                }

                for (int i = 0; i < v; i++)
                {
                    if (discTime[i] == -1)
                    {
                        List<int> component = new List<int>();
                        TarjanComponentsUtil(i, discTime, lowTime, st, stackMember, components);
                        components.Add(component);
                    }
                }

                return components;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (panel1.Height == 163)
            {
                panel1.Height = 60;
                label1.Visible = false;
            }

            else
            {
                panel1.Height = 163;
                label1.Visible = true;
            }
        }
    }
}


