namespace Test.IdentityServer4.EFStores.Data.Entities
{
    public class Node
    {
        public bool Enabled { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Node;
            if (other == null)
            {
                return false;
            }

            return this.Enabled.Equals(other.Enabled);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    public sealed class Permissions
    {
        public InformationNode Information { get; set; }
        public SettingsNode Settings { get; set; }

        public Permissions()
        {
            Information = new InformationNode();
            Settings = new SettingsNode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Permissions;
            if (other == null)
            {
                return false;
            }

            return this.Information.Equals(other.Information) && this.Settings.Equals(other.Settings);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class InformationNode
    {
        public Node AuditTrail { get; set; }

        public InformationNode()
        {
            AuditTrail = new Node();
        }

        public override bool Equals(object obj)
        {
            var other = obj as InformationNode;
            if (other == null)
            {
                return false;
            }

            return this.AuditTrail.Equals(other.AuditTrail);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class SettingsNode
    {
        public UsersAndRolesNode UsersAndRoles { get; set; }
        public NetworkSettingsNode NetworkSettings { get; set; }
        public Node Organisations { get; set; }

        public SettingsNode()
        {
            UsersAndRoles = new UsersAndRolesNode();
            NetworkSettings = new NetworkSettingsNode();
            Organisations = new Node();
        }

        public override bool Equals(object obj)
        {
            var other = obj as SettingsNode;
            if (other == null)
            {
                return false;
            }

            return this.UsersAndRoles.Equals(other.UsersAndRoles) && this.NetworkSettings.Equals(other.NetworkSettings) && this.Organisations.Equals(other.Organisations);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class UsersAndRolesNode
    {
        public Node Users { get; set; }
        public Node Roles { get; set; }

        public UsersAndRolesNode()
        {
            Users = new Node();
            Roles = new Node();
        }

        public override bool Equals(object obj)
        {
            var other = obj as UsersAndRolesNode;
            if (other == null)
            {
                return false;
            }

            return this.Users.Equals(other.Users) && this.Roles.Equals(other.Roles);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class NetworkSettingsNode
    {
        public Node EmailServerSettings { get; set; }
        public Node SMSServerSettings { get; set; }

        public NetworkSettingsNode()
        {
            EmailServerSettings = new Node();
            SMSServerSettings = new Node();
        }

        public override bool Equals(object obj)
        {
            var other = obj as NetworkSettingsNode;
            if (other == null)
            {
                return false;
            }

            return this.EmailServerSettings.Equals(other.EmailServerSettings) && this.SMSServerSettings.Equals(other.SMSServerSettings);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    
}
