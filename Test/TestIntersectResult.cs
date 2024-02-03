using Godot;
using Godot.Collections;

namespace NoromaGD.Test
{
    public partial class TestIntersectResult : Node2D
    {
        public override void _Ready()
        {
            var query = PhysicsRayQueryParameters2D.Create(GlobalPosition, GlobalPosition + Vector2.Right);
            Dictionary result = GetWorld2D().DirectSpaceState.IntersectRay(query);

            Variant outValue;
            result.TryGetValue("collider", out outValue);
            GD.Print($"collider : {outValue.VariantType}"); //Object
            result.TryGetValue("collider_id", out outValue);
            GD.Print($"collider_id : {outValue.VariantType}"); //Int
            result.TryGetValue("normal", out outValue);
            GD.Print($"normal : {outValue.VariantType}"); //Vector2
            result.TryGetValue("position", out outValue);
            GD.Print($"position : {outValue.VariantType}"); //Vector2
            result.TryGetValue("rid", out outValue);
            GD.Print($"rid : {outValue.VariantType}"); //Rid
            result.TryGetValue("shape", out outValue);
            GD.Print($"shape : {outValue.VariantType}"); //Int

            var hit = this.Raycast(GlobalPosition, Vector2.Right, 10);
            if (hit)
            {
                GD.Print($"Collider : {hit.Collider}");
                GD.Print($"ColliderId : {hit.ColliderId}");
                GD.Print($"Normal : {hit.Normal}");
                GD.Print($"Position : {hit.Position}");
                GD.Print($"Rid : {hit.Rid}");
                GD.Print($"Shape : {hit.Shape}");
            }

            if (hit)
            {
                var body = hit.Collider as StaticBody2D;
                GodotObject owner = body.ShapeOwnerGetOwner((uint)hit.Shape);
                GD.Print(owner);
            }
        }
    }
}