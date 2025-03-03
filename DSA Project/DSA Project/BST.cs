using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_Project
{
    class BSTNode
    {
        public Account Account { get; set; }
        public BSTNode Left { get; set; }
        public BSTNode Right { get; set; }

        public BSTNode(Account account)
        {
            Account = account;
            Left = null;
            Right = null;
        }
    }

    class BST
    {
        private BSTNode root;

        public void Insert(Account account)
        {
            root = InsertRec(root, account);
        }

        private BSTNode InsertRec(BSTNode root, Account account)
        {
            if (root == null)
            {
                return new BSTNode(account);
            }

            if (string.Compare(account.Name, root.Account.Name, StringComparison.OrdinalIgnoreCase) < 0)
            {
                root.Left = InsertRec(root.Left, account);
            }
            else
            {
                root.Right = InsertRec(root.Right, account);
            }

            return root;
        }

        public List<Account> Search(string name)
        {
            var results = new List<Account>();
            SearchRec(root, name, results);
            return results;
        }

        private void SearchRec(BSTNode root, string name, List<Account> results)
        {
            if (root == null)
            {
                return;
            }

            if (root.Account.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(root.Account);
            }

            SearchRec(root.Left, name, results);
            SearchRec(root.Right, name, results);
        }
    }
}
