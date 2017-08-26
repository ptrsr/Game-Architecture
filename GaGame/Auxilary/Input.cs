/*
 * User: Eelco
 * Date: 5/13/2017
 * Time: 4:36 PM
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

static public class Input
{
	public enum KeyAttack
    {
        Up,
        Down,
        Pressed
    }

    private struct KeyEntry
    {
        public Keys      key;
        public KeyAttack attack;

        public KeyEntry(Keys pKey, KeyAttack pAttack)
        {
            key = pKey;
            attack = pAttack;
        }
    }

	static public void Init( Form  pForm )
    {
		Key.Init( pForm );
	}

    static public void Resolve()
    {
        foreach (KeyValuePair<KeyEntry, Action> keyAction in functions)
        {
            if (
                keyAction.Key.attack == KeyAttack.Down && Key.Enter(keyAction.Key.key) ||
                keyAction.Key.attack == KeyAttack.Pressed && Key.Pressed(keyAction.Key.key)
               )
                keyAction.Value();
        }
    }

    static public void Bind(Keys key, Action func, KeyAttack attack = KeyAttack.Pressed)
    {
        Action action;
        KeyEntry entry = new KeyEntry(key, attack);

        if (functions.TryGetValue(entry, out action))
        {
            action += func;
            functions[entry] = action;
        }
        else
            functions.Add(entry, func);
    }

    static public void UnBind(Keys key, Action func, KeyAttack attack = KeyAttack.Pressed)
    {
        Action action;
        KeyEntry entry = new KeyEntry(key, attack);


        if (functions.TryGetValue(entry, out action))
        {
            action -= func;

            if (action == null)
                functions.Remove(entry);
            else
                functions[entry] = action;
        }
    }

    static private Dictionary<KeyEntry, Action> functions = new Dictionary<KeyEntry, Action>();

    static public class Key {

		static private Dictionary<Keys, float> keys = new Dictionary<Keys, float>();

		static public void Init( Form  pForm ) {
			pForm.KeyDown += new KeyEventHandler( Down );
			pForm.KeyUp += new KeyEventHandler( Up );
		}
		
	
		static private void Down( Object sender, KeyEventArgs e ) 
		{
			Keys key = e.KeyCode;
			float time = 0;
			keys.TryGetValue( key, out time );
			if( time == 0 ) { // non exists or upped
				keys[ key ] = Time.Real;
			}
		}
		
		static private void Up( Object sender, KeyEventArgs e ) 
		{
			keys[ e.KeyCode ] = 0;
		}
		
		static public bool Enter( Keys key ) 
		{
			float time = 0;
			if( keys.TryGetValue( key, out time ) ) {
				return time > Time.Now - Time.Step;
			}
			return false;
		}
		static public bool Pressed( Keys key ) 
		{
            float time = 0;
			if( keys.TryGetValue( key, out time ) ) {
				return time > 0;
			}
			return false;
		}
	}
}


